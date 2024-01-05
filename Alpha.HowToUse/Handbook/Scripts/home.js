/// <reference path="auto-suggestion.ts" />
var HomePage = /** @class */ (function () {
    function HomePage($, autoSuggestion) {
        var _this = this;
        this.enterKeyCode = 13;
        this.init = function () {
            _this.registerEvents();
            _this.autoSuggestion.init({
                elmSearch: _this.$('#txt-search'),
                boxSuggestion: _this.$('.box-suggestion'),
                suggestUrl: _this.$("#RootPath").val() + "Article/Suggest",
                selectedSuggestion: _this.selectedSuggestion
            });
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
        };
        this.searchArticle = function () {
            var keyword = _this.$('#txt-search').val();
            location.href = _this.$("#RootPath").val() + "Article/Search?keyword=" + keyword;
        };
        this.selectedSuggestion = function (id) {
            location.href = _this.$("#RootPath").val() + "Article/GetBySlug?slug=" + id;
        };
        this.$ = $;
        this.autoSuggestion = autoSuggestion;
    }
    return HomePage;
}());
;
$(function () {
    var autoSuggestion = new AutoSuggestion($);
    var page = new HomePage($, autoSuggestion);
    page.init();
});
//# sourceMappingURL=home.js.map