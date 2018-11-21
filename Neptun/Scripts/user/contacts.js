$(document).ready(function () {
    //textarea expand

    $('textarea[max-rows]').each(function () {
        var textarea = $(this);

        var minRows = Number(textarea.attr('rows'));
        var maxRows = Number(textarea.attr('max-rows'));

        // clone the textarea and hide it offscreen
        // TODO: copy all the styles
        var textareaClone = $('<textarea/>', {
            rows: minRows,
            maxRows: maxRows,
            width: 2000,
            class: textarea.attr('class')
        }).css({
            position: 'absolute',
            left: -$(document).width() * 2
        }).insertAfter(textarea);

        var textareaCloneNode = textareaClone.get(0);

        $(window).resize(function () {
            if (textarea.val() != "") textarea.trigger('input');
        });

        textarea.on('input', function () {
            // copy the input from the real textarea
            textareaClone.val(textarea.val());

            // set as small as possible to get the real scroll height
            textareaClone.attr('rows', 1);

            // save the real scroll height
            var scrollHeight = textareaCloneNode.scrollHeight;

            // increase the number of rows until the content fits
            for (var rows = minRows; rows < maxRows; rows++) {
                textareaClone.attr('rows', rows);

                if (textareaClone.height() > scrollHeight) {
                    break;
                }
            }

            // copy the rows value back to the real textarea
            textarea.attr('rows', textareaClone.attr('rows'));
        }).trigger('input');
    });

    //label animation
    $('form label').each(function () {
        var label = $(this);
        var input = label.next();
        if (input.is("input") || input.is("textarea")) {
            $(input.focus(function () {
                label.addClass("active");
                input.trigger("input");
                if (input.attr('data-validate') != "false") label.css("color", "rgb(72, 90, 253)");
            })
            );
            $(input.focusout(function () {
                if (input.attr('data-validate') != "false") label.css("color", "#888");
                if (input.val() == "") {
                    label.removeClass("active");
                }
            })
            );
        }
    });

    $('#email').on('input', function () {
        var email = $(this);
        validate(email, 100, true, validateEmail);
    });

    $('#firstName').on('input', function () {
        var firstName = $(this);
        validate(firstName, 25, true, null);
    });

    $('#lastName').on('input', function () {
        var lastName = $(this);
        validate(lastName, 25, false, null);
    });

    $('#message').on('input', function () {
        var message = $(this);
        validate(message, 1000, true, null);
    });

    $('#phone-number').on('input', function () {
        var phone = $(this);
        validate(phone, null, false, validatePhone);
    });

    function validate(input, maxLength, required, validationFunction) {
        inputVal = input.val();
        var label = $('label[for=' + input.attr('id') + ']');
        if (maxLength != null && inputVal.length >= maxLength) {
            validationFailed(label, input);
            label.text(input.attr('data-maxLenght-message'));
        }
        else if (validationFunction != null && inputVal != "" && !validationFunction(inputVal)) {
            validationFailed(label, input);
            label.text(input.attr('data-incorrect-message'));
        }
        else if (required === true && inputVal.trim() == "") {
            validationFailed(label, input);
            label.text(input.attr('data-correct-message') + " (обязательно)*");
        }
        else {
            label.css("color", "rgb(72, 90, 253)");
            label.text(input.attr('data-correct-message'));
            input.attr('data-validate', true);
            return true;
        }
        return false;
    }

    function validationFailed(label, input) {
        label.css("color", "red");
        input.attr('data-validate', false);
    }

    function validateEmail(email) {
        var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return re.test(email);
    }

    function validatePhone(email) {
        var re = /^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$/im;
        return re.test(email);
    }
});

//google maps
function initMap() {
    var uluru = { lat: 46.526458, lng: 30.650840 };
    var map = new google.maps.Map(document.getElementById('map'), {
        zoom: 13,
        center: uluru
    });
    var marker = new google.maps.Marker({
        position: uluru,
        map: map
    });
}

$('#feedback-submit-btn').click(function (e) {
    e.preventDefault();

    var valid = true;

    $('#feedback-form label').each(function () {
        var input = $(this).next();
        if (input.is("input") || input.is("textarea")) {
            if (input.attr('data-validate') == "false") {
                valid = false;
                input.trigger('input');
            }
        }
    });

    if (valid === false) return;

    var data = {
        FirstName: $('#firstName').val().trim(),
        LastName: $('#lastName').val().trim(),
        Email: $('#email').val().trim(),
        PhoneNuber: $('#phone-number').val().trim(),
        Message: $('#message').val().trim()
    };

    console.log(data);

    $.ajax({
        type: "POST",
        url: "/FeedBacks/Index",
        data: data,
        success: function () {
            window.location.replace("/Company");
        },
        error: function () {
            alert("Извините, ваше сообщение не отправлено, попробуйте связаться с нами другим образом");
            window.location.replace("/Company");
        }
    });
});