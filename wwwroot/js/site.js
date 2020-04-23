//global variables
let titlesInputsBlock = $('#titlesInputs');

//handlers
$('#addTitleButton').click(function() {
    let inputsCount =  titlesInputsBlock.children('input').length;
    appendTitleInput(inputsCount);
});

$(".custom-file-input").on("change", function() {
    let fileName = $(this).val().split("\\").pop();
    $(this).siblings(".custom-file-label").addClass("selected").html(fileName);
});

//functions
function appendTitleInput(index) {
    titlesInputsBlock.append(
        '<input id="titlesInput-' + index + '" name="UserTitles['+ index +']" type="text" class="form-control mb-1" placeholder="Example Title" data-val="true" data-val-required="Title must not be empty"/>'
    );
}

