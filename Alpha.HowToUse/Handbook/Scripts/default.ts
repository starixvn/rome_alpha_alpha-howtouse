/// <reference path="auto-suggestion.ts" />

class DefaultPage {
    $: JQueryStatic;
    topWindow: any;
    autoSuggestion: AutoSuggestion;

    enterKeyCode = 13;
    currentUrlCookieName = 'DFFKDHFJKLDHFJDH2';
    recheckInIntervalMiliseconds = 30000;

    constructor($: JQueryStatic, topWindow: any, autoSuggestion: AutoSuggestion) {
        this.$ = $;
        this.topWindow = topWindow;
        this.autoSuggestion = autoSuggestion;
    }

    init = () => {
        this.registerEvents();

        this.autoSuggestion.init({
            elmSearch: this.$('#txt-search'),
            boxSuggestion: this.$('.box-suggestion'),
            suggestUrl: this.$("#RootPath").val() + "Article/Suggest",
            selectedSuggestion: this.selectedSuggestion
        });

        var setCurrentURL = this.$("#SetCurrentURL").val() === '1';
        if (setCurrentURL) {
            var currentUrl = this.getCurrentUrl();
            var mainSrc = currentUrl && currentUrl.indexOf("_Authorization/SignIn.aspx") === -1
                ? currentUrl : "Home";
            this.$("#ifr-main").attr("src", mainSrc);
        }
    };
    registerEvents = () => {
        this.$('.btn-search').click(() => this.searchArticle());
        this.$('#txt-search').keyup((ev) => {
            var keycode = (ev.keyCode ? ev.keyCode : ev.which);
            if (keycode === this.enterKeyCode) {
                this.searchArticle();
                return;
            }

            this.autoSuggestion.suggest();
        });

        this.$(".language-container").click(this.showHideLanguage);

        var self = this;
        this.$(".dropdown-language > li a").click(function () {
            var s = self.$(this).attr('data-val');
            self.changeLanguage(s);
        });

        this.$(".item-cat").click(function () {
            var $this = self.$(this);
            if ($this.closest('li').find('>.item-cat').hasClass('open')) {
                $this.closest('li').find('>.item-cat').removeClass('open');
                return;
            }

            $this.closest('.list-item-cat').find('>.item-cat').removeClass('open');
            $this.closest('li').find('>.item-cat').addClass('open');
        });

        this.$(".item-slug").click(function () {
            self.$(".item-cat.active").removeClass('active');
            var $this = self.$(this);
            $this.parents('.item-cat').addClass('active');                 
        });

        this.$("#ifr-main").on('load', () => {
            self.cacheCurrentUrl();
        });

        this.reCheckin();
        setTimeout(() => { this.reCheckin(); }, this.recheckInIntervalMiliseconds);
    };

    searchArticle = () => {
        var keyword = this.$('#txt-search').val();
        this.$("#ifr-main").attr("src", this.$("#RootPath").val() + "Article/Search?keyword=" + keyword);
    };

    showHideLanguage = () => {
        var ddl = this.$(".dropdown-language");

        if (ddl.hasClass('open')) {
            ddl.removeClass('open');
        } else {
            ddl.addClass('open');
        }
    };

    changeLanguage = (s) => {
        this.$.ajax({
            type: 'GET',
            url: "Language/Change?selected=" + s,
            success: (data, textStatus, jqXHR) => {
                this.topWindow.location.reload(true);
            },
            complete: function (jqXHR, textStatus) { }
        });
    };

    getCurrentUrl = () => {
        return localStorage.getItem(this.currentUrlCookieName);
    };
    cacheCurrentUrl = () => {
        try {
            var mainFrame = <HTMLIFrameElement>document.getElementById("ifr-main");
            var mainFrameDocument = mainFrame.contentWindow || mainFrame.contentDocument;
            var currentUrl = mainFrameDocument && mainFrameDocument.location.href || mainFrame.src;
            localStorage.setItem(this.currentUrlCookieName, currentUrl);
        } catch (e) { }
    };
    reCheckin = () => {
        // Post with return result in JSON format
        this.$.cookie('ssl', location.href.indexOf('https://') === 0, { expires: 30, path: '/' });
        var self = this;

        this.$.ajax({
            type: "POST",
            url: this.$("#OldMainRootPath").val() + '_Authorization/Handlers/ReCheckin.ashx',
            data: { onid: this.$("#OnUserId").val() },
            success: (result) => {
                try {
                    result = eval("(" + result + ")");
                    var errCode = result.errCode;
                    var errMsg = result.errMsg;

                    if (errCode !== 0) {
                        //self.topWindow.location.reload(true);
                        self.topWindow.location = self.$("#OldMainRootPath").val() + "/_Authorization/SignIn.aspx";
                    }
                }
                catch (e) {
                }
            },
            complete: () => {
                setTimeout(() => { this.reCheckin(); }, this.recheckInIntervalMiliseconds);
            }
        });
    };

    selectedSuggestion = (id) => {
        var url = this.$("#RootPath").val() + "Article/GetBySlug?slug=" + id;
        this.$("#ifr-main").attr("src", url);
    };
};

$(function () {
    const autoSuggestion = new AutoSuggestion($);
    const page = new DefaultPage($, window.top, autoSuggestion);
    page.init();
});
