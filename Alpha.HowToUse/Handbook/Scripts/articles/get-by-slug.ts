
class ArticleGetBySlug {
    $: JQueryStatic;

    constructor($: JQueryStatic) {
        this.$ = $;
    }

    init = () => {
        this.makeFloatingContentBox();
        this.registerEvents();
    };

    registerEvents = () => {
    };

    makeFloatingContentBox = () => {
        var headlings = this.$(".article-content h1, .article-content h2, .article-content h3");
        if (headlings.length === 0) {
            return;
        }

        var html = '<div class="floating-box"><ul>';
        headlings.each((i, e) => {
            var $e = this.$(e);
            var t = $e.html();
            var newH = this.$('<div>' + t + '</div>');
            newH.find('a').remove();
            html += '<li><a href="#' + $e.find('a').attr('id') + '">' + newH.html() + '</a></li>';
        });

        html += '</ul></div>';

        this.$('.main-article').append(html);
    }
};

$(function () {
    const page = new ArticleGetBySlug($);
    page.init();
});
