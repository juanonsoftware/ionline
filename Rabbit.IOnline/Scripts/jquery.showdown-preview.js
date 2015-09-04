(function ($) {
    // options
    //{
    // source: source element selector
    // defaultMessage: default message to display when the source is empty
    //}

    $.fn.previewShowdown = function (options) {

        var $textarea = $(options.source);
        var $preview = $(this);

        $textarea.keyup(function () {
            $preview.html(convert($textarea.val()));
        }).trigger('keyup');

        function convert(text) {
            var converter = new showdown.Converter(),
                html = converter.makeHtml(text);

            if (html.length < 1 && options.defaultMessage) {
                html = converter.makeHtml(options.defaultMessage);
            }

            return html;
        }

        return this;
    };

}(jQuery));