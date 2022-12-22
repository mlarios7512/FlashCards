function checkFront(cardId, originalString, originalOppositeString)
{
    let frontInput = $("#front-" + cardId).val();
    if (frontInput == originalString) {
        $("#front-header-" + cardId).removeClass("edited-subheader-indication");

        let curOppositeInput = $("#back-" + cardId).val()
        if (curOppositeInput == originalOppositeString) {
            $("#card-" + cardId).removeClass("edited-card");
        }  
    }
    else {
        $("#front-header-" + cardId).addClass("edited-subheader-indication");
        $("#card-" + cardId).addClass("edited-card");
    }
}

function undoFront(cardId, originalString, originalOppositeSideString)
{
    $("#front-" + cardId).val(originalString);
    $("#front-header-" + cardId).removeClass("edited-subheader-indication");

    let curOppositeInput = $("#back-" + cardId).val()
    if (curOppositeInput == originalOppositeSideString)
    {
        $("#card-" + cardId).removeClass("edited-card");
    }
    console.log("undo triggered");
}






function checkBack(cardId, originalString, originalOppositeString)
{
    let frontInput = $("#back-" + cardId).val();
    if (frontInput == originalString) {
        $("#back-header-" + cardId).removeClass("edited-subheader-indication");

        let curOppositeInput = $("#front-" + cardId).val()
        if (curOppositeInput == originalOppositeString)
        {
            $("#card-" + cardId).removeClass("edited-card");
        }  
    }
    else {
        $("#back-header-" + cardId).addClass("edited-subheader-indication");
        $("#card-" + cardId).addClass("edited-card");
    }
}


function undoBack(cardId, originalString, originalOppositeSideString)
{
    $("#back-" + cardId).val(originalString);
    $("#back-header-" + cardId).removeClass("edited-subheader-indication");

    let curOppositeInput = $("#front-" + cardId).val()
    if (curOppositeInput == originalOppositeSideString) {
        $("#card-" + cardId).removeClass("edited-card");
    }
    console.log("undo triggered (back)");
}








function toggleDeleteStatus(checkboxId)
{
    //If it has the "marked-for-delete-card" class
    if ($("#checkbox-" + checkboxId).is(":checked"))
    {
        markForDelete(checkboxId);
        
    }
    else
    {
        unmarkForDelete(checkboxId);
    }
}

function unmarkForDelete(cardId)
{
    $("#card-" + cardId).removeClass("marked-for-delete-card");

    //"If" edits have been made... (write logic later)
    /*   $("#card-" + cardId).addClass("edited-card")*/

    //"Else" give it the neutral color.
/*    $("#card-" + cardId).addClass("unchanged-card");*/

    $("#front-" + cardId).prop('disabled', false);
    $("#back-" + cardId).prop('disabled', false);

    $("#front-" + cardId).css("background-color", "white");
    $("#back-" + cardId).css("background-color", "white");
}

function markForDelete(cardId)
{
    //$("#card-" + cardId).removeClass("unchanged-card");
    //$("#card-" + cardId).removeClass("edited-card");

    
    $("#card-" + cardId).addClass("marked-for-delete-card");

    $("#front-" + cardId).prop('disabled', true);
    $("#back-" + cardId).prop('disabled', true);

    $("#front-" + cardId).css("background-color", "#dcdcdc");
    $("#back-" + cardId).css("background-color", "#dcdcdc");
}











function markAllForDelete()
{

}