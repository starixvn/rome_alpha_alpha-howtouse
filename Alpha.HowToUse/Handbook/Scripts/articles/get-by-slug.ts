
class ArticleGetBySlug {
    $: JQueryStatic;

    constructor($: JQueryStatic) {
        this.$ = $;
    }

    init = () => {
        this.makeFloatingContentBox();
        // this.correctImageUrls();
    };


    makeFloatingContentBox = () => {
        var headlings = this.$(".article-content h1, .article-content h2, .article-content h3");
        if (headlings.length === 0) {
            return;
        }

        var html = '<ul class="list-content">';
        var hs = ['h1', 'h2', 'h3'];

        headlings.each((i, e) => {
            var $e = this.$(e);
            var t = $e.html();
            var newH = this.$('<div>' + t + '</div>');
            newH.find('a').remove();

            hs.forEach(h => {
                if ($e.is(h)) {
                    html += '<li><' + h + '><a href="#' + $e.find('a').attr('id') + '">' +
                        newH.html() +
                        '</a></' + h + '></li>';
                    return;
                }
            })
        });

        html += '</ul>';

        this.$('#float-content').append(html);
    };

    correctImageUrls = () => {
        var imgs = this.$(".article-content img");
        var rootUrl = this.$("#RootPath").val();

        imgs.each((i, e) => {
            var $e = this.$(e);
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
};

$(function () {
    const page = new ArticleGetBySlug($);
    page.init();
});
