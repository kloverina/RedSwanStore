﻿onSaveChanges = function (xhr) {
    //TODO: Add display of incorrect input messages

    // received data is json so I parsed it and made detailed description here especially for you!
    // login result instance looks like if you do the next thing:
    // let saveChangesResult = {
    //      isCorrectLogin: <bool>,
    //      isCorrectUrl: <bool>,
    //      isCorrectName: <bool>,
    //      isCorrectSurname: <bool>,
    //      isCorrectOldPassword: <bool>,
    //      isCorrectNewPassword: <bool>
    // }
    let saveChangesResult = JSON.parse(xhr.responseText);
    
    if (!saveChangesResult.isCorrectLogin) {
        console.log("Проблемы с логином");
        return;
    }
    else if (!saveChangesResult.isCorrectUrl) {
        console.log("Проблемы с личной ссылкой");
        return;
    }
    else if (!saveChangesResult.isCorrectName) {
        console.log("Проблемы с именем");
        return;
    }
    else if (!saveChangesResult.isCorrectSurname) {
        console.log("Проблемы с фамилией");
        return;
    }
    else if (!saveChangesResult.isCorrectOldPassword) {
        console.log("Неверный пароль");
        return;
    }
    else if (!saveChangesResult.isCorrectNewPassword) {
        console.log("Проблемы с новым паролем");
        return;
    }
    
    window.location.reload(true);
}



document.querySelector('button[type=reset]').addEventListener('click', function (e) {
   window.location.reload(true); 
});


/* DYNAMIC SHOWING THE PROFILE LINK WHILE INPUT */
let dynamicLinkElement = document.querySelector('#account_url + span > span');
let accountLinkInput = document.querySelector('#account_url');
accountLinkInput.addEventListener('input', function() {
    dynamicLinkElement.textContent = accountLinkInput.value.trimEnd();
});