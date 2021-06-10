String.prototype.count=function(c) {
    let result = 0, i = 0;

    for(i;i<this.length;i++)
        if(this[i]===c)
            result++;

    return result;
};

function checkboxClick(labels) {
    for(let i = 0; i< labels.length; i++){
        let label = labels[i];



        label.onclick= function (){
            let checkbox = label.querySelector("input");
            let custom_checkbox = label.querySelector('div');
            if (checkbox.checked)
                custom_checkbox.classList.add('checked');
            else
                custom_checkbox.classList.remove('checked');

            if (!checkbox.hasAttribute('value')) {
                checkbox.setAttribute('value', 'true');
            }
            else {
                checkbox.setAttribute(
                    'value',
                    checkbox.getAttribute('value') === 'true' ? 'false' : 'true'
                );
            }
        }
    }
}

//clicks on custom checkboxes
let labels = document.querySelectorAll('label.custom-checkbox');
if (labels)
    checkboxClick(labels);



/* VALIDATION */
setOnInputListener = function(elem, replaceRegex) {
    elem.addEventListener('input', function() {
        let maxlength = parseInt(elem.getAttribute('maxlength'));
        let maxVal = parseFloat(elem.getAttribute('max'));

        if (maxVal) {
            if (parseFloat(elem.value) > maxVal)
                elem.value = elem.value.substring(0, elem.value.length - 1);
        }

        if (maxlength) {
            if (elem.value.length > maxlength)
                elem.value = elem.value.substring(0, maxlength);
        }

        elem.value = elem.value.replace(replaceRegex, '');
    });
}


let nameInput = document.querySelector('#Name');
let developerInput = document.querySelector('#Developer');
let gameUrlInput = document.querySelector('#GameUrl');
let genresCheckboxes = document.querySelectorAll('input[type=checkbox][name=genre]');
let featuresCheckboxes = document.querySelectorAll('input[type=checkbox][name=feature]');
let priceInput = document.querySelector('#Price');
let discountInput = document.querySelector('#Discount');
let discountEndDateInput = document.querySelector('#DiscountEndDate');
let releaseDateInput = document.querySelector('#ReleaseDate');
let ratingInput = document.querySelector('#Rating');
let shortDescriptionTextarea = document.querySelector('#ShortDescription');
let detailedDescriptionTextarea = document.querySelector('#DetailedDescription');
let legalInfoTextarea = document.querySelector('#LegalInfo');
let legalInfoLinkInput = document.querySelector('#LegalInfoLink');
let minCpuInput = document.querySelector('#MinCpu');
let minRamInput = document.querySelector('#MinRam');
let minGpuInput = document.querySelector('#MinGpu');
let maxCpuInput = document.querySelector('#MaxCpu');
let maxRamInput = document.querySelector('#MaxRam');
let maxGpuInput = document.querySelector('#MaxGpu');
let diskSpaceInput = document.querySelector('#DiskSpace');
let directXInput = document.querySelector('#DirectX');
let supportedOsesInput = document.querySelector('#SupportedOses');
let extraInfoInput = document.querySelector('#ExtraInfo');
let supportedVoiceLanguagesInput = document.querySelector('#SupportedVoiceLanguages');
let supportedTextLanguagesInput = document.querySelector('#SupportedTextLanguages');
let coverLinkInput = document.querySelector('#Cover');
let screenshotsTextarea = document.querySelector('#Screenshots');
let trailersInput = document.querySelector('#Trailers');
let editGameBtn = document.querySelector('#save_changes');
let cancelBtn = document.querySelector('#reset_changes');

setOnInputListener(gameUrlInput, /[^a-zA-Z0-9-]/g);
setOnInputListener(priceInput, /[^[0-9.]/g);
setOnInputListener(discountInput, /[^[0-9]/g);
setOnInputListener(ratingInput, /[^[0-9]/g);
setOnInputListener(minRamInput, /[^[0-9]/g);
setOnInputListener(maxRamInput, /[^[0-9]/g);
setOnInputListener(diskSpaceInput, /[^[0-9]/g);
setOnInputListener(directXInput, /[^[0-9]/g);



/* DYNAMIC SHOWING THE GAME LINK WHILE INPUT */
let dynamicLinkElement = document.querySelector('#dynamic-link');
gameUrlInput.addEventListener('input', function() {
    dynamicLinkElement.textContent = gameUrlInput.value.trimEnd();
});


/* PROCESS THE DATA ON SUBMIT */
showInvalid = (elem) => elem.style.cssText = "border-color: red";
showValid = (elem) => elem.style.cssText = "";

checkInputData = function () {
    if (nameInput.value.length > 100) {
        showInvalid(nameInput);
        return false;
    }
    else
        showValid(nameInput);


    if (developerInput.value.length > 100) {
        showInvalid(developerInput);
        return false;
    }
    else
        showValid(developerInput);

    if (gameUrlInput.value.length > 100) {
        showInvalid(gameUrlInput);
        return false;
    }
    else
        showValid(gameUrlInput);

    if (priceInput.value.count('.') > 1) {
        showInvalid(priceInput);
        return false;
    }
    else
        showValid(priceInput);

    return true;
}


/* SENDING DATA TO SERVER */
editGameBtn.addEventListener('click',   function (e) {
    if (!checkInputData()) {
        e.preventDefault();
        return;
    }

    let name = nameInput.value.trimEnd();
    let gameUrl = gameUrlInput.value.trimEnd();
    let developer = developerInput.value.trimEnd();

    let cover = coverLinkInput.value.trimEnd();
    let price = parseFloat(priceInput.value.trimEnd());
    let discount = (parseFloat(discountInput.value.trimEnd()) / 100).toString();
    let discountEndDate = discountEndDateInput.value;
    let releaseDate = releaseDateInput.value;
    let rating = parseInt(ratingInput.value.trimEnd());
    let shortDescription = shortDescriptionTextarea.value.trimEnd();
    let detailedDescription = detailedDescriptionTextarea.value.trimEnd();
    let legalInfo = `<div class="legal-info__data"><span class="legal-info__main-info">${legalInfoTextarea.value.trimEnd()}</span></div><div class="legal-info__data"><a href="${legalInfoLinkInput.value.trimEnd()}" class="legal-info__link">Политика конфиденциальности</a></div>`;

    let minCpu = minCpuInput.value.trimEnd();
    let maxCpu = maxCpuInput.value.trimEnd();
    let minRamMB = parseInt(minRamInput.value.trimEnd());
    let maxRamMB = parseInt(maxRamInput.value.trimEnd());
    let diskSpaceMB = parseInt(diskSpaceInput.value.trimEnd());
    let directX = parseInt(directXInput.value.trimEnd());
    let minGpu = minGpuInput.value.trimEnd();
    let maxGpu = maxGpuInput.value.trimEnd();
    let supportedOses = supportedOsesInput.value.trimEnd();
    let extraInfo = extraInfoInput.value.trimEnd();
    let supportedVoiceLanguages = supportedVoiceLanguagesInput.value.trimEnd();
    let supportedTextLanguages = supportedTextLanguagesInput.value.trimEnd();

    let trailers = trailersInput.value.trimEnd();
    let screenshots = screenshotsTextarea.value.trimEnd();

    let genres = [];
    for (let i = 0; i < genresCheckboxes.length; i++) {
        let checkbox = genresCheckboxes[i];

        if (checkbox.getAttribute('value') === 'true')
            genres.push(checkbox.getAttribute('data-url'))
    }

    let features = [];
    for (let i = 0; i < featuresCheckboxes.length; i++) {
        let checkbox = featuresCheckboxes[i];

        if (checkbox.getAttribute('value') === 'true')
            features.push(checkbox.getAttribute('data-url'))
    }


    let ajaxData = {
        name: name,
        gameUrl: gameUrl,
        developer: developer,
        cover: cover,
        price: price,
        discount: discount,
        discountEndDate: discountEndDate,
        releaseDate: releaseDate,
        rating: rating,
        shortDescription: shortDescription,
        detailedDescription: detailedDescription,
        legalInfo: legalInfo,
        minCpu: minCpu,
        maxCpu: maxCpu,
        minRamMB: minRamMB,
        maxRamMB: maxRamMB,
        diskSpaceMB: diskSpaceMB,
        directX: directX,
        minGpu: minGpu,
        maxGpu: maxGpu,
        supportedOses: supportedOses,
        extraInfo: extraInfo,
        supportedVoiceLanguages: supportedVoiceLanguages,
        supportedTextLanguages: supportedTextLanguages,
        trailers: trailers,
        screenshots: screenshots,
        features: features,
        genres: genres
    }

    $.ajax({
        type: 'post',
        url: '/games-management/save-edited-game',
        data: ajaxData,
        dataType: 'text',
        error: (xhr) => console.log(xhr.responseText),
        success: (text) => {
            if (!text.startsWith('/game'))
                console.log(text);
            else
                window.location.replace(text);
        }
    })
});


cancelBtn.addEventListener('click', function() {
   $.ajax({
       type: 'post',
       url: '/games-management/cancel',
       success: (text) => {
           window.location.replace(text);
       }
   });
});