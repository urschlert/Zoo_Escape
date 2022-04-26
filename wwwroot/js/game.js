(function($){

    var zooBreakout = {};
  
    // game saving
    zooBreakout.savingObject = {};
  
    zooBreakout.savingObject.deck = [];
  
    // an array to store which card is removed by storing their index.
    zooBreakout.savingObject.removedCards = [];
  
    // store the counting elapsed time.
    zooBreakout.savingObject.currentElapsedTime = 0;
  
    // deck data
    zooBreakout.deck = [
      'a', 'b',
      'c', 'd',
      'e', 'f',
      'g', 'h',
      'i', 'j',
      'k', 'l',
    ];
  
    function shuffle() {
      return 0.5 - Math.random();
    }
  
    function selectCard() {
      // we do nothing if there are already one card flipped.
      if ($(".card-flipped").size() > 0) {
        return;
      }
      $(this).addClass("card-flipped");
      // removing flipped card 0.7s later.
      if ($(".card-flipped").size() === 1) {
        setTimeout(removingCards, 1000);
      }
    }
  
    function removingCards() {
        $(".card-flipped").removeClass("card-flipped").addClass("card-removed");
        $(".card-removed").bind("transitionend", removeTookCards);
    }


    function removeTookCards() {
      // add each removed card into the array which store which cards are removed
      $(".card-removed").each(function(){
        zooBreakout.savingObject.removedCards.push($(this).data("card-index"));
        $(this).remove();
      });
  
      // check if all cards are removed and show game over
      if ($(".card").length === 0) {
        gameover();
      }
    }
  
    function saveSavingObject() {
       // save the encoded saving object into local storage
       localStorage["savingObject"] = JSON.stringify(zooBreakout.savingObject);
    }
  
    function gameover() {
      // stop the timer
      clearInterval(zooBreakout.timer);
  
      // display the elapsed time in the game over popup
      $(".score").html($("#elapsed-time"));
  
      // load the saved last score and save time from local storage
      var lastScore = localStorage.getItem("last-score");
  
      // check if there is no any saved record
      lastScoreObj = JSON.parse(lastScore);
      if (lastScoreObj === null) {
        // create an empty record if there is no any saved record
        lastScoreObj = {"savedTime": "no record", "score": 0};
      }
      var lastElapsedTime = lastScoreObj.score;
  
      // convert the elapsed seconds into minute:second format
      // calculate the minutes and seconds from elapsed time
      var minute = Math.floor(lastElapsedTime / 60);
      var second = lastElapsedTime % 60;
  
      // add padding 0 if minute and second is less then 10
      if (minute < 10) minute = "0" + minute;
      if (second < 10) second = "0" + second;
  
      // display the last elapsed time in game over popup
      $(".last-score").html(minute+":"+second);
  
      // display the saved time of last score
      var savedTime = lastScoreObj.savedTime;
      $(".saved-time").html(savedTime);
  
      // get the current datetime
      var currentTime = new Date();
  
      // convert the date time into string.
      var now = currentTime.toLocaleString();
  
      //construct the object of datetime and game score
      var obj = { "savedTime": now, "score":
        zooBreakout.elapsedTime};
  
      // save the score into local storage
      localStorage.setItem("last-score", JSON.stringify(obj));
  
      // show the game over popup
      $("#popup").removeClass("hide");
  
      // Ribbon
      if (lastElapsedTime === 0 || zooBreakout.elapsedTime < lastElapsedTime) {
        $(".ribbon").removeClass("hide");
      }
  
      //at last, we clear the saved savingObject
      localStorage.removeItem("savingObject");
    }
  
    function countTimer() {
      zooBreakout.elapsedTime++;
  
      // save the current elapsed time into savingObject.
      zooBreakout.savingObject.currentElapsedTime =
      zooBreakout.elapsedTime;
  
      // calculate the minutes and seconds from elapsed time
      var minute = Math.floor(zooBreakout.elapsedTime / 60);
      var second = zooBreakout.elapsedTime % 60;
  
      // add padding 0 if minute and second is less then 10
      if (minute < 10) minute = "0" + minute;
      if (second < 10) second = "0" + second;
  
      // display the elapsed time
      $("#elapsed-time").html(minute+":"+second);
  
      // save the game progress
      saveSavingObject();
    }
  
    // Returns the saved savingObject from the local storage.
    function savedSavingObject() {
       // returns the saved saving object from local storage
       var savingObject = localStorage["savingObject"];
       if (savingObject !== undefined) {
          savingObject = JSON.parse(savingObject);
       }
       return savingObject;
    }
  
    $(document).ready(function(){
  
      // reset the elapsed time to 0.
      zooBreakout.elapsedTime = 0;
  
      // start the timer
      zooBreakout.timer = setInterval(countTimer, 1000);
  
      // shuffle the deck.
      zooBreakout.deck.sort(shuffle);
  
      // re-create the saved deck
      var savedObject = savedSavingObject();
      if (savedObject !== undefined) {
        zooBreakout.deck = savedObject.deck;
      }
  
      // reset the elapsed time to 0.
      zooBreakout.elapsedTime = 0;
  
      // restore the saved elapsed time
      if (savedObject !== undefined)
      {
         zooBreakout.elapsedTime = savedObject.currentElapsedTime;
         zooBreakout.savingObject.currentElapsedTime = savedObject.
         currentElapsedTime;
      }
  
      // copying the deck into saving object.
      zooBreakout.savingObject.deck = zooBreakout.deck.slice();
  
      // User number
      userNumber = 12;

      for(var i=1;i<userNumber;i++){
        $(".card:first-child").clone().appendTo("#cards");
      }
  
      $("#cards").children().each(function(index) {
        var x = ($(this).width()  + 20) * (index % (userNumber/2) );
        var y = ($(this).height() + 20) * Math.floor(index / (userNumber/2) );
        $(this).css("transform", "translateX(" + x + "px) translateY(" + y + "px)");
  
        // get a pattern from the shuffled deck
        var pattern = zooBreakout.deck.pop();
  
        // visually apply the pattern on the card's back side.
        $(this).find(".back").addClass(pattern);
  
        // embed the pattern data into the DOM element.
        $(this).attr("data-pattern",pattern);
  
        // save the index into the DOM element, so we know which is the next card.
        $(this).attr("data-card-index",index);
  
        // listen the click event on each card DIV element.
        $(this).click(selectCard);
      });
  
      // removed cards that were removed in savedObject.
      if (savedObject !== undefined) {
        zooBreakout.savingObject.removedCards =
          savedObject.removedCards;
        // find those cards and remove them.
        for(var i in zooBreakout.savingObject.removedCards) {
          $(".card[data-card-index="+zooBreakout.savingObject.
            removedCards[i]+"]").remove();
        }
      }
  
    });
  })(jQuery);