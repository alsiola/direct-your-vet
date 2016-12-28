define(function () {
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
        }
    }
});