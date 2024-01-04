var DefaultPage = /** @class */ (function () {
    function DefaultPage($) {
        var _this = this;
        this.init = function () {
            _this.registerEvents();
        };
        this.registerEvents = function () {
            _this.$('.btn-search').click(_this.searchArticle);
        };
        this.searchArticle = function () {
            var keyword = _this.$('$txt-search').val();
            alert(keyword);
        };
        this.$ = $;
    }
    return DefaultPage;
}());
;
$(function () {
    var page = new DefaultPage($);
    page.init();
});
//# sourceMappingURL=default.js.map