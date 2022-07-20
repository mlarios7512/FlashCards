// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

////ReviewIndividualSet (below)***************************
$(document).ready(function () {
    $(".card-answer").hide();
    $(".hide-answer").hide();

    $(".switch-to-prev-card").hide()

    for (let otherCards = 1; otherCards < 10; otherCards++)
        $("#card-" + otherCards).hide();

    $(".finished-review-message").hide();

    let curCard = 0;
    let maxCardsPerSet = 2;

    $("#card-count").text("Card " + (curCard+1) + " out of ");

    //1 time set up stuff (above)--------------------------


    $(".show-answer").click(function () {
        $(".card-answer").slideDown();
        $(".show-answer").hide();
        $(".hide-answer").show();
    });

    $(".hide-answer").click(function () {
        $(".card-answer").slideUp();
        $(".show-answer").show();
        $(".hide-answer").hide();
    })


  
    $(".switch-to-next-card").click(function () {
        $("#card-" + curCard).hide();

        if (curCard < maxCardsPerSet)
        {
            curCard++;
            $(".switch-to-prev-card").show();
            if (curCard >= maxCardsPerSet)
            {
                $("#total-card-count").hide();
                $(".review-card").hide();
                $(".finished-review-message").show();
                /*$("#back-to-last-card").show();*/

                $(".switch-to-prev-card").show();
            }

            $("#card-count").text("Card " + (curCard + 1) + " out of ");

            $("#card-" + curCard).show();
        }
        

            $(".card-answer").hide();
        
    })

    $(".switch-to-prev-card").click(function () {
        $("#card-" + curCard).hide();

        curCard--;
        if (curCard < maxCardsPerSet)
        {
            $(".finished-review-message").hide();
            $("#total-card-count").show();
        }
        
        if (curCard == 0)
            $(".switch-to-prev-card").hide()

        $("#card-" + curCard).show();
        $("#card-count").text("Card " + (curCard + 1) + " out of ");

        $(".card-answer").hide();
    })
});

//ReviewIndividualSet (above)******************************