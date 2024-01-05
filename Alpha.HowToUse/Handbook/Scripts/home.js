var HomePage = /** @class */ (function () {
    function HomePage($) {
        var _this = this;
        this.enterKeyCode = 13;
        this.init = function () {
            _this.registerEvents();
        };
        this.registerEvents = function () {
            _this.$('.btn-search').click(function () { return _this.searchArticle(); });
            _this.$('#txt-search').keyup(function (ev) {
                var keycode = (ev.keyCode ? ev.keyCode : ev.which);
                if (keycode === _this.enterKeyCode) {
                    _this.searchArticle();
                }
            });
        };
        this.searchArticle = function () {
            var keyword = _this.$('#txt-search').val();
            var language = _this.$('#Language').val();
            location.href = _this.$("#RootPath").val() + "/Article/Search?keyword=" + keyword + "&language=" + language;
        };
        this.$ = $;
    }
    return HomePage;
}());
;
$(function () {
    var page = new HomePage($);
    page.init();
});
//# sourceMappingURL=home.js.map