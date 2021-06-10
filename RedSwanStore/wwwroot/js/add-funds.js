let cvcInput = document.querySelector('.cvc input');
let monthInput = document.querySelector('.expiration-month input');
let yearInput = document.querySelector('.expiration-year input');
let cardHolderInput = document.querySelector('.card-holder input');
let cardNumberInput = document.querySelector('.card-number input');
let moneyInput = document.querySelector('#money-amount')

let submitBtn = document.querySelector('#commit-transaction');


preventInput = function (input, replaceRegex) {
    let maxlength = parseInt(input.getAttribute('maxlength'));
    let maxVal = parseInt(input.getAttribute('max'));

    if (maxVal) {
        if (input.value > maxVal)
            input.value = input.value.substring(0, maxlength - 1);
    }

    if (maxlength) {
        if (input.value.length > maxlength)
            input.value = input.value.substring(0, maxlength);
    }

    input.value = input.value.replace(replaceRegex, '');
}


cvcInput.addEventListener('input', function(e) {
    preventInput(this, /[^0-9]/g);
});

monthInput.addEventListener('input', function(e) {
    preventInput(this, /[^0-9]/g);
});

yearInput.addEventListener('input', function(e) {
    preventInput(this, /[^0-9]/g);
});

cardHolderInput.addEventListener('input', function(e) {
    preventInput(this, /[^A-Z\s]/g);
});

cardNumberInput.addEventListener('input', function(e) {
    preventInput(this, /[^0-9]/g);
});

moneyInput.addEventListener('input', function (e) {
   preventInput(this, /[^0-9]/g);
});


submitBtn.addEventListener('click', function () {
   $.ajax({
       type: 'post',
       url: '/profile/update-balance',
       data: {
           moneyAmount: moneyInput.value
       },
       success: () => {
           window.location.replace('/thanks-for-purchase/funds-successfully-added');
       }
   }) 
});