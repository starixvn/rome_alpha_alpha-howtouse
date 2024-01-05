var ArticleGetBySlug = /** @class */ (function () {
    function ArticleGetBySlug($) {
        var _this = this;
        this.init = function () {
            _this.makeFloatingContentBox();
            _this.registerEvents();
        };
        this.registerEvents = function () {
        };
        this.makeFloatingContentBox = function () {
            var headlings = _this.$(".article-content h1, .article-content h2, .article-content h3");
            if (headlings.length === 0) {
                return;
            }
            var html = '<div class="floating-box"><ul>';
            headlings.each(function (i, e) {
                var $e = _this.$(e);
                var t = $e.html();
                var newH = _this.$('<div>' + t + '</div>');
                newH.find('a').remove();
                html += '<li><a href="#' + $e.find('a').attr('id') + '">' + newH.html() + '</a></li>';
            });
            html += '</ul></div>';
            _this.$('.main-article').append(html);
        };
        this.$ = $;
    }
    return ArticleGetBySlug;
}());
;
$(function () {
    var page = new ArticleGetBySlug($);
    page.init();
});
//# sourceMappingURL=get-by-slug.js.map