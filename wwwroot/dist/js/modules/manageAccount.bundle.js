webpackJsonp([4],{

/***/ 17:
/***/ function(module, exports, __webpack_require__) {

"use strict";
/* WEBPACK VAR INJECTION */(function(nm) {var __WEBPACK_AMD_DEFINE_RESULT__;"use strict";

!(__WEBPACK_AMD_DEFINE_RESULT__ = function (require) {

    var msg = $('#message').val();
    if (msg.length > 0) {
        nm.notyMessage(msg);
    }

    var ManageViewModel = __webpack_require__(7);
    ko.applyBindings(new ManageViewModel());
}.call(exports, __webpack_require__, exports, module), __WEBPACK_AMD_DEFINE_RESULT__ !== undefined && (module.exports = __WEBPACK_AMD_DEFINE_RESULT__));
/* WEBPACK VAR INJECTION */}.call(exports, __webpack_require__(0)))

/***/ },

/***/ 7:
/***/ function(module, exports, __webpack_require__) {

"use strict";
/* WEBPACK VAR INJECTION */(function(nm) {var __WEBPACK_AMD_DEFINE_RESULT__;"use strict";

!(__WEBPACK_AMD_DEFINE_RESULT__ = function () {
    return function ManageViewModel() {
        var self = this;
        self.resendEmailConfirm = function () {
            $.post("http://" + window.location.host + "/Account/ResendEmailVerification", null, function (response) {
                if (response.success) {
                    nm.notyMessage("Verification email sent.");
                } else {
                    nm.notyMessage("Email sending failed.");
                }
            });
        };
    };
}.call(exports, __webpack_require__, exports, module), __WEBPACK_AMD_DEFINE_RESULT__ !== undefined && (module.exports = __WEBPACK_AMD_DEFINE_RESULT__));
/* WEBPACK VAR INJECTION */}.call(exports, __webpack_require__(0)))

/***/ }

},[17]);