(function ($) {
    // options
    //{
    // source: source element selector
    // defaultMessage: default message to display when the source is empty
    //}

    $.fn.previewShowdown = function (options) {

        var $preview = $(this);

        if (options.source) {
            var $textarea = $(options.source);

            $textarea.keyup(function () {
                var text = $textarea.val();

                if (text.length < 1 && options.defaultMessage) {
                    text = options.defaultMessage;
                }

                $preview.html(convert(text));
            }).trigger('keyup');
        } else {
            $preview.html(convert(options.defaultMessage));
        }

        function convert(text) {
            var converter = new showdown.Converter(),
                html = converter.makeHtml(text);

            return html;
        }

        return this;
    };

}(jQuery));