class HomePage {
    $: JQueryStatic;
    enterKeyCode = 13;

    constructor($: JQueryStatic) {
        this.$ = $;
    }

    init = () => {
        this.registerEvents();
    };

    registerEvents = () => {
        this.$('.btn-search').click(() => this.searchArticle());
        this.$('#txt-search').keyup((ev) => {
            var keycode = (ev.keyCode ? ev.keyCode : ev.which);
            if (keycode === this.enterKeyCode) {
                this.searchArticle();
            }
        });
    };

    searchArticle = () => {
        var keyword = this.$('#txt-search').val();
        var language = this.$('#Language').val();
        location.href = this.$("#RootPath").val() + "/Article/Search?keyword=" + keyword + "&language=" + language;
    };
};

$(function () {
    const page = new HomePage($);
    page.init();
});
