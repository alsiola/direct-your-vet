define(function (require) {

    var SendSMSViewModel = require("./sendSMSViewModel");

    $('#tokenize')
            .tokenize(
                {
                    placeholder: "Phone numbers go here...",
                    onAddToken: function (value, text, e) {
                        console.log(text);
                        text = text.replace(/ /g, '');
                        console.log("ws trim: " + text);
                        if (!Number(text)) {
                            e.tokenRemove(value);
                        }
                        var $counter = $('#smsCounter');
                        $counter.val(Number(Number($counter.val()) + 1)).change();
                    },
                    onRemoveToken: function () {
                        var $counter = $('#smsCounter');
                        $counter.val(Number(Number($counter.val()) - 1)).change();
                    }
                });


    ko.applyBindings(new SendSMSViewModel());
});