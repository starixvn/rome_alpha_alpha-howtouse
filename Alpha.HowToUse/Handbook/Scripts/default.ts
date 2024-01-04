class DefaultPage {
    $: JQueryStatic;

    constructor($: JQueryStatic) {
        this.$ = $;
    }

    init = () => {
        this.registerEvents();
    };
    registerEvents = () => {
        this.$('.btn-search').click(this.searchArticle);
    };

    searchArticle = () => {
        var keyword = this.$('$txt-search').val();

        alert(keyword);
    };
};


$(function () {
    const page = new DefaultPage($);
    page.init();
});
