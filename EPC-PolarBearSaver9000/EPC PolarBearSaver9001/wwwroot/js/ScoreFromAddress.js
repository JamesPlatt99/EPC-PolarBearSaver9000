$(document).ready(function () {
    var generateScoreButton = document.getElementById("GenerateScoreButton");
    generateScoreButton.addEventListener("click", function (evt) {
        GetScore();
        evt.preventDefault();
    });
});

//TODO: move request into site.js function to share across files
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
            var score = request.response;
            DisplayScore(score);
            UpdateScoreColour(score);
        };
    }

}

function DisplayScore(score) {
    var scoreLabel = document.getElementById("EPCScoreLabel");
    scoreLabel.innerHTML = score;
}

function UpdateScoreColour(score) {
    var scoreBackground = document.getElementById("EPCScoreLabel").parentElement.parentElement;
    scoreBackground.style.backgroundColor = CalculateScoreColour(score);
}

function CalculateScoreColour(score){
    var r = CalculateColourWeighting(100 - score);
    var g = CalculateColourWeighting(score);
    return 'rgb(' + r + ',' + g + ',' + 0 + ')';
}

function CalculateColourWeighting(score) {
    if (score > 50) {
        return 255;
    }
    return (score / 50) * 255;
}