var DefaultPage = /** @class */ (function () {
    function DefaultPage($, topWindow) {
        var _this = this;
        this.enterKeyCode = 13;
        this.currentUrlCookieName = 'DFFKDHFJKLDHFJDH2';
        this.recheckInIntervalMiliseconds = 30000;
        this.init = function () {
            _this.registerEvents();
            var currentUrl = _this.getCurrentUrl();
            if (currentUrl) {
                _this.$("#ifr-main").attr("src", currentUrl);
            }
        };
        this.registerEvents = function () {
            _this.$('.btn-search').click(function () { return _this.searchArticle(); });
            _this.$('#txt-search').keyup(function (ev) {
                var keycode = (ev.keyCode ? ev.keyCode : ev.which);
                if (keycode === _this.enterKeyCode) {
                    _this.searchArticle();
                }
            });
            _this.$(".language-container").click(_this.showHideLanguage);
            var self = _this;
            _this.$(".dropdown-language > li a").click(function () {
                var s = self.$(this).attr('data-val');
                self.changeLanguage(s);
            });
            _this.$(".item-cat > .icon-right").click(function () {
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
            setTimeout(function () { _this.reCheckin(); }, _this.recheckInIntervalMiliseconds);
        };
        this.searchArticle = function () {
            var keyword = _this.$('#txt-search').val();
            var language = _this.$('#Language').val();
            _this.$("#ifr-main").attr("src", "./Article/Search?keyword=" + keyword + "&language=" + language);
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
                            self.topWindow.location.reload(true);
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
        this.$ = $;
        this.topWindow = topWindow;
    }
    return DefaultPage;
}());
;
$(function () {
    var page = new DefaultPage($, window.top);
    page.init();
});
//# sourceMappingURL=default.js.map