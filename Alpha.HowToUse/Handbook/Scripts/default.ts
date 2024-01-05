class DefaultPage {
    $: JQueryStatic;
    topWindow: any;

    enterKeyCode = 13;
    currentUrlCookieName = 'DFFKDHFJKLDHFJDH2';
    recheckInIntervalMiliseconds = 30000;

    constructor($: JQueryStatic, topWindow: any) {
        this.$ = $;
        this.topWindow = topWindow;
    }

    init = () => {
        this.registerEvents();

        var currentUrl = this.getCurrentUrl();
        if (currentUrl) {
            this.$("#ifr-main").attr("src", currentUrl);
        }
    };
    registerEvents = () => {
        this.$('.btn-search').click(() => this.searchArticle());
        this.$('#txt-search').keyup((ev) => {
            var keycode = (ev.keyCode ? ev.keyCode : ev.which);
            if (keycode === this.enterKeyCode) {
                this.searchArticle();
            }
        });

        this.$(".language-container").click(this.showHideLanguage);

        var self = this;
        this.$(".dropdown-language > li a").click(function () {
            var s = self.$(this).attr('data-val');
            self.changeLanguage(s);
        });

        this.$(".item-cat > .icon-right").click(function () {
            var $this = self.$(this);
            if ($this.closest('li').find('>.item-cat').hasClass('open')) {
                $this.closest('li').find('>.item-cat').removeClass('open');
                return;
            }

            $this.closest('.list-item-cat').find('>.item-cat').removeClass('open');
            $this.closest('li').find('>.item-cat').addClass('open');
        });

        this.$("#ifr-main").on('load', () => {
            self.cacheCurrentUrl();
        });

        setTimeout(() => { this.reCheckin(); }, this.recheckInIntervalMiliseconds);
    };

    searchArticle = () => {
        var keyword = this.$('#txt-search').val();
        var language = this.$('#Language').val();
        this.$("#ifr-main").attr("src", "./Article/Search?keyword=" + keyword + "&language=" + language);
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
                        self.topWindow.location.reload(true);
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
};

$(function () {
    const page = new DefaultPage($, window.top);
    page.init();
});
