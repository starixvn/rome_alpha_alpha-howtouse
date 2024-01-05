class AutoSuggestion {
    $: JQueryStatic;
    minSearchLength = 2;
    options: any;

    constructor($: JQueryStatic) {
        this.$ = $;
    }

    init = (options) => {
        this.options = options;
    };

    suggest = () => {
        var keyword = this.options.elmSearch.val();
        if (keyword.length <= this.minSearchLength) {
            this.options.boxSuggestion.addClass('hidden');
            return;
        }

        this.search(keyword);
    };

    search = (keyword) => {
        var self = this;

        this.$.ajax({
            type: 'GET',
            url: this.options.suggestUrl + "?keyword=" + keyword,
            success: (data, textStatus, jqXHR) => {
                var ul = this.options.boxSuggestion.find(">ul");
                ul.empty();

                if (data.length) {
                    data.forEach(e => {
                        ul.append('<li data-id="' + e.Id + '">' + e.Value + '</li>');
                    });

                    ul.find(">li").click(function () {
                        var $this = self.$(this);
                        self.selectedSuggestion($this);
                    });
                } else {
                    ul.append('<li>' + this.$("#NoRecords").val() + '</li>');
                }

                this.options.boxSuggestion.removeClass('hidden');
            },
            complete: function (jqXHR, textStatus) { }
        });
    };

    selectedSuggestion = (elm) => {
        var id = elm.attr("data-id");
        this.options.selectedSuggestion(id);
    };
};
