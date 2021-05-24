
let form = document.querySelector('.form');
let inputs = form.querySelectorAll('input')
let email = form.querySelector('.form-element__email'); //delete me later
let password = form.querySelector('.form_element__password'); //and me too
let pass_match = form.querySelector('input.form_element__checkpass');
let submit_button = form.querySelector('.form_element__button');
let pass_match_warning = form.querySelector('p.form_element__checkpass');


function isValid(inputs) {
    let valid = true;
    
    
    inputs.forEach(function (input, i, inputs) {
        if (input.value === '' || !input.validity.valid ) {
            valid = false;
        }
    })

    if (!passMatch()) valid = false;
    
    return valid;
}

function passMatch(){
    
    if (!pass_match) 
        return true;
    if(password.value !== pass_match.value && pass_match.value !== ''){
        pass_match_warning.classList.add('active');
        return false
    }
    else
        pass_match_warning.classList.remove('active');
    
    
    return true;
    
}


form.addEventListener("input", function (event) {
    
   if (isValid(inputs))
       submit_button.classList.add('active');
   else
       submit_button.classList.remove('active');
})


form.onsubmit = function(event) {
    event.preventDefault();
    console.log('Форма отправлена!');
    if (password)
        console.log("Email:", email.value, ", password: ", password.value);
    else
        console.log("Email:", email.value);
};

