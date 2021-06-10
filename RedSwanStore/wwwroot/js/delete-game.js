let deleteGameBtn = document.querySelector('#save_changes');
let cancelBtn = document.querySelector('#reset_changes');

cancelBtn.addEventListener('click', function() {
    $.ajax({
        type: 'post',
        url: '/games-management/cancel',
        success: (text) => {
            window.location.replace(text);
        }
    });
});


deleteGameBtn.addEventListener('click', function (){
   $.ajax({
       type: 'post',
       url: '/games-management/remove-from-store',
       success: (text) => {window.location.replace(text)}
   }) 
});
