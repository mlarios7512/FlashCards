
$("#visible-add-a-card").click(function () {

    if (document.getElementById("potential-card-form") == null) {

        let cardtemplate = `
        <form id="potential-card-form">
            <div class="col-4">
                <div class="card not-ready-card" id="potential-card">
                    <div class="side-indication">
                        <h6>Front</h6>
                        <textarea cols="29" id="potential-front" onkeyup="checkCardValidity()"></textarea>
                    </div>
                    <div class="side-indication">
                        <h6>Back</h6>
                        <textarea cols="29" id="potential-back" onkeyup="checkCardValidity()"></textarea>
                    </div>
                </div>
            </div>
        </form>`;

        $(cardtemplate).insertBefore("#visible-add-card-container");

        $("#visible-add-a-card").hide();
    }
    else {
        console.log("HTML form already exists.")
    }

   
});


function checkCardValidity()
{
    let front = $("#potential-front").val();
    let back = $("#potential-back").val();
    console.log("check card validity (front):" + front);
    console.log("check card validity (back):" + back); 

    const re = /^[a-zA-Z\. \s \_ \,\-\']+$/;

    if (re.test(front) == true && re.test(back) == true) {
        $("#potential-card").removeClass("not-ready-card");
        $("#potential-card").addClass("ready-to-add-card");
        console.log("card should be ready!")
        document.getElementById("submit-potential-card-btn").disabled = false; 
    }
    else
    {
        document.getElementById("submit-potential-card-btn").disabled = true;
        $("#potential-card").removeClass("ready-to-add-card");
        $("#potential-card").addClass("not-ready-card");
    }

}


$("#submit-potential-card-btn").click(function () {
    let formToSubmit = document.getElementById("potential-card-form");
    formToSubmit.submit();
});