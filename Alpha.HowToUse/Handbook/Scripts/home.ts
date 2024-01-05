/// <reference path="auto-suggestion.ts" />

class HomePage {
    $: JQueryStatic;
    autoSuggestion: AutoSuggestion;

    enterKeyCode = 13;

    constructor($: JQueryStatic, autoSuggestion: AutoSuggestion) {
        this.$ = $;
        this.autoSuggestion = autoSuggestion;
    }

    init = () => {
        this.registerEvents();

        this.autoSuggestion.init({
            elmSearch: this.$('#txt-search'),
            boxSuggestion: this.$('.box-suggestion'),
            suggestUrl: this.$("#RootPath").val() + "Article/Suggest",
            selectedSuggestion: this.selectedSuggestion
        });
    };

    registerEvents = () => {
        this.$('.btn-search').click(() => this.searchArticle());
        this.$('#txt-search').keyup((ev) => {
            var keycode = (ev.keyCode ? ev.keyCode : ev.which);
            if (keycode === this.enterKeyCode) {
                this.searchArticle();
                return;
            }

            this.autoSuggestion.suggest();
        });
    };

    searchArticle = () => {
        var keyword = this.$('#txt-search').val();
        location.href = this.$("#RootPath").val() + "Article/Search?keyword=" + keyword;
    };

    selectedSuggestion = (id) => {
        location.href = this.$("#RootPath").val() + "Article/GetBySlug?slug=" + id;
    };
};

$(function () {
    const autoSuggestion = new AutoSuggestion($);

    const page = new HomePage($, autoSuggestion);
    page.init();
});
