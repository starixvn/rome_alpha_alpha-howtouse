var ArticleGetBySlug = /** @class */ (function () {
    function ArticleGetBySlug($) {
        var _this = this;
        this.init = function () {
            _this.makeFloatingContentBox();
            // this.correctImageUrls();
        };
        this.makeFloatingContentBox = function () {
            var headlings = _this.$(".article-content h1, .article-content h2, .article-content h3");
            if (headlings.length === 0) {
                return;
            }
            var html = '<ul class="list-content">';
            var hs = ['h1', 'h2', 'h3'];
            headlings.each(function (i, e) {
                var $e = _this.$(e);
                var t = $e.html();
                var newH = _this.$('<div>' + t + '</div>');
                newH.find('a').remove();
                hs.forEach(function (h) {
                    if ($e.is(h)) {
                        html += '<li><' + h + '><a href="#' + $e.find('a').attr('id') + '">' +
                            newH.html() +
                            '</a></' + h + '></li>';
                        return;
                    }
                });
            });
            html += '</ul>';
            _this.$('#float-content').append(html);
        };
        this.correctImageUrls = function () {
            var imgs = _this.$(".article-content img");
            var rootUrl = _this.$("#RootPath").val();
            imgs.each(function (i, e) {
                var $e = _this.$(e);
                var s = $e.attr('src');
                var regex = '/api/images/get/';
                var start = s.indexOf(regex);
                if (start === -1) {
                    return;
                }
                var name = s.substring(start + regex.length);
                $e.attr('src', rootUrl + "Image/Get?name=" + name);
            });
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