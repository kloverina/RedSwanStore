let popupWindow = document.querySelector('.game-popup-window');
let cvcInput = popupWindow.querySelector('.cvc input');
let monthInput = popupWindow.querySelector('.expiration-month input');
let yearInput = popupWindow.querySelector('.expiration-year input');
let cardHolderInput = popupWindow.querySelector('.card-holder input');
let cardNumberInput = popupWindow.querySelector('.card-number input');


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
   preventInput(this, /[^0-9]/g) 
});

let ajaxOptions = {
    type: 'post',
    url: '/game/purchase-by-card',
    success: () => {
        window.location.replace('/thanks-for-purchase');
    }
}