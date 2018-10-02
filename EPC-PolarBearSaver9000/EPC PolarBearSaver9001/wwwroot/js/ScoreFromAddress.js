$(document).ready(function () {
    var generateScoreButton = document.getElementById("GenerateScoreButton");
    generateScoreButton.addEventListener("click", function (evt) {
        GetScore();
        evt.preventDefault();
    });
});

//move request into site.js function to share across files
function GetScore() {
    var postcode = document.getElementById("PostcodeTextBox").value.replace(" ", "");
    var addressDropdown = document.getElementById("AddressLineDropdown");
    if (addressDropdown !== undefined && addressDropdown !== null) {
        var address = addressDropdown.value;
        var searchTerm = URL + SCORE_END_POINT + postcode + "/" + address;
        var request = new XMLHttpRequest();
        request.open('GET', searchTerm);
        request.responseType = 'json';
        request.send();

        request.onload = function () {
            console.log(request.response);
            var score = request.response;
            DisplayScore(score);
            console.log(score);
        };
    }

}

function DisplayScore(score) {
    var scoreLabel = document.getElementById("EPCScoreLabel");
    scoreLabel.innerHTML = score;
}