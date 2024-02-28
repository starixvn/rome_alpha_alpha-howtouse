/// <reference path="auto-suggestion.ts" />
var DefaultPage = /** @class */ (function () {
    function DefaultPage($, topWindow, autoSuggestion) {
        var _this = this;
        this.enterKeyCode = 13;
        this.currentUrlCookieName = 'DFFKDHFJKLDHFJDH2';
        this.recheckInIntervalMiliseconds = 30000;
        this.init = function () {
            _this.registerEvents();
            _this.autoSuggestion.init({
                elmSearch: _this.$('#txt-search'),
                boxSuggestion: _this.$('.box-suggestion'),
                suggestUrl: _this.$("#RootPath").val() + "Article/Suggest",
                selectedSuggestion: _this.selectedSuggestion
            });
            var currentUrl = _this.getCurrentUrl();
            var mainSrc = currentUrl && currentUrl.indexOf("_Authorization/SignIn.aspx") === -1
                ? currentUrl : "Home";
            _this.$("#ifr-main").attr("src", mainSrc);
        };
        this.registerEvents = function () {
            _this.$('.btn-search').click(function () { return _this.searchArticle(); });
            _this.$('#txt-search').keyup(function (ev) {
                var keycode = (ev.keyCode ? ev.keyCode : ev.which);
                if (keycode === _this.enterKeyCode) {
                    _this.searchArticle();
                    return;
                }
                _this.autoSuggestion.suggest();
            });
            _this.$(".language-container").click(_this.showHideLanguage);
            var self = _this;
            _this.$(".dropdown-language > li a").click(function () {
                var s = self.$(this).attr('data-val');
                self.changeLanguage(s);
            });
            _this.$(".item-cat").click(function () {
                var $this = self.$(this);
                if ($this.closest('li').find('>.item-cat').hasClass('open')) {
                    $this.closest('li').find('>.item-cat').removeClass('open');
                    return;
                }
                $this.closest('.list-item-cat').find('>.item-cat').removeClass('open');
                $this.closest('li').find('>.item-cat').addClass('open');
            });
            _this.$("#ifr-main").on('load', function () {
                self.cacheCurrentUrl();
            });
            _this.reCheckin();
            setTimeout(function () { _this.reCheckin(); }, _this.recheckInIntervalMiliseconds);
        };
        this.searchArticle = function () {
            var keyword = _this.$('#txt-search').val();
            _this.$("#ifr-main").attr("src", "./Article/Search?keyword=" + keyword);
        };
        this.showHideLanguage = function () {
            var ddl = _this.$(".dropdown-language");
            if (ddl.hasClass('open')) {
                ddl.removeClass('open');
            }
            else {
                ddl.addClass('open');
            }
        };
        this.changeLanguage = function (s) {
            _this.$.ajax({
                type: 'GET',
                url: "Language/Change?selected=" + s,
                success: function (data, textStatus, jqXHR) {
                    _this.topWindow.location.reload(true);
                },
                complete: function (jqXHR, textStatus) { }
            });
        };
        this.getCurrentUrl = function () {
            return localStorage.getItem(_this.currentUrlCookieName);
        };
        this.cacheCurrentUrl = function () {
            try {
                var mainFrame = document.getElementById("ifr-main");
                var mainFrameDocument = mainFrame.contentWindow || mainFrame.contentDocument;
                var currentUrl = mainFrameDocument && mainFrameDocument.location.href || mainFrame.src;
                localStorage.setItem(_this.currentUrlCookieName, currentUrl);
            }
            catch (e) { }
        };
        this.reCheckin = function () {
            // Post with return result in JSON format
            _this.$.cookie('ssl', location.href.indexOf('https://') === 0, { expires: 30, path: '/' });
            var self = _this;
            _this.$.ajax({
                type: "POST",
                url: _this.$("#OldMainRootPath").val() + '_Authorization/Handlers/ReCheckin.ashx',
                data: { onid: _this.$("#OnUserId").val() },
                success: function (result) {
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
                complete: function () {
                    setTimeout(function () { _this.reCheckin(); }, _this.recheckInIntervalMiliseconds);
                }
            });
        };
        this.selectedSuggestion = function (id) {
            var url = _this.$("#RootPath").val() + "Article/GetBySlug?slug=" + id;
            _this.$("#ifr-main").attr("src", url);
        };
        this.$ = $;
        this.topWindow = topWindow;
        this.autoSuggestion = autoSuggestion;
    }
    return DefaultPage;
}());
;
$(function () {
    var autoSuggestion = new AutoSuggestion($);
    var page = new DefaultPage($, window.top, autoSuggestion);
    page.init();
});
//# sourceMappingURL=default.js.map