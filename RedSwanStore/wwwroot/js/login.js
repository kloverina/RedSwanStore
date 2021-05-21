
let form = document.querySelector('.form');
let email = form.querySelector('.form-element__email');
let password = form.querySelector('.form_element__password');
let submit_button = form.querySelector('.form_element__button');

form.addEventListener("input", function (event) {
    
  
    if (email.validity.valid)
        if (!password || password.validity.valid)
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

