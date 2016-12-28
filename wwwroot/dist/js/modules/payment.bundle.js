webpackJsonp([6],{

/***/ 18:
/***/ function(module, exports) {

"use strict";
'use strict';

$(function () {
    Stripe.setPublishableKey("@ApiKeys.GetStripePublicKey()");

    var $form = $('#payment-form');

    $form.submit(function (event) {
        $form.find('.submit').prop('disabled', true);

        Stripe.card.createToken($form, stripeResponseHandler);

        return false;
    });

    $('#qty').keydown(function (event) {
        // Allow: backspace, delete, tab, escape, and enter
        if (event.keyCode === 46 || event.keyCode === 8 || event.keyCode === 9 || event.keyCode === 27 || event.keyCode === 13 ||
        // Allow: Ctrl+A
        event.keyCode === 65 && event.ctrlKey === true ||
        // Allow: . ,
        event.keyCode === 188 || event.keyCode === 190 || event.keyCode === 110 ||
        // Allow: home, end, left, right
        event.keyCode >= 35 && event.keyCode <= 39) {
            // let it happen, don't do anything
            return;
        } else {
            // Ensure that it is a number and stop the keypress
            if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
                event.preventDefault();
            }

            return;
        }
    }).keyup(function () {
        refreshTotal();
    });

    $('#purchaseoptions').change(function () {
        refreshTotal();
    }).change();
});

function refreshTotal() {
    $('#total').val('£' + $('#qty').val() * $('#purchaseoptions > option:selected').attr('data-price'));
}

function stripeResponseHandler(status, response) {
    var $form = $('#payment-form');
    var $serverForm = $('#server-form');

    if (response.error) {
        $form.find('.payment-errors').text(response.error.message);
        $form.find('.submit').prop('disabled', false);
    } else {
        var token = response.id;
        $serverForm.append($('<input type="hidden" name="stripeToken">').val(token));
        $serverForm.append($('<input type="hidden" name="purchaseType">').val($('#purchaseoptions').val()));
        $serverForm.append($('<input type="hidden" name="purchaseQty">').val($('#qty').val()));
        $serverForm.get(0).submit();
    }
};

/***/ }

},[18]);