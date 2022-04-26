// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace ZooBreakout.Components
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "/Users/anthony/Documents/GitHub/CS4500_Group4/_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/Users/anthony/Documents/GitHub/CS4500_Group4/_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "/Users/anthony/Documents/GitHub/CS4500_Group4/_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "/Users/anthony/Documents/GitHub/CS4500_Group4/_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "/Users/anthony/Documents/GitHub/CS4500_Group4/_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "/Users/anthony/Documents/GitHub/CS4500_Group4/_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "/Users/anthony/Documents/GitHub/CS4500_Group4/_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "/Users/anthony/Documents/GitHub/CS4500_Group4/_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "/Users/anthony/Documents/GitHub/CS4500_Group4/_Imports.razor"
using ZooBreakout;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "/Users/anthony/Documents/GitHub/CS4500_Group4/_Imports.razor"
using ZooBreakout.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "/Users/anthony/Documents/GitHub/CS4500_Group4/_Imports.razor"
using ZooBreakout.Components;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "/Users/anthony/Documents/GitHub/CS4500_Group4/_Imports.razor"
using ZooBreakout.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 13 "/Users/anthony/Documents/GitHub/CS4500_Group4/_Imports.razor"
using ZooBreakout.Data;

#line default
#line hidden
#nullable disable
    public partial class SecondRound : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 185 "/Users/anthony/Documents/GitHub/CS4500_Group4/Components/SecondRound.razor"
       
    [Parameter]
    public EventCallback<Tuple<int, int, int>> SecondRoundComplete { get; set; }
    [Parameter]
    public string Name { get; set; } = "";
    public int CorrectUnwinnables { get; set; } = 0;
    public int Wins { get; set; } = 0;
    public Deck TheDeck { get; set; }
    public bool UserWon { get; set; } = false;
    public int NumberOfRounds { get; set; } = 0;
    public bool CanStillPlay { get; set; } = true;
    public bool GameWinnable{ get; set; }
    public string[] Chevrons { get; set; } = new string[7];
    public string[] Draggables { get; set; } = new string[] {
        "greenleft", "greenright", "orangeleft", "orangeright",
        "purpleleft", "purpleright", "blue", "yellow"
    };
    public string UnwinnableString { get; set; } = "";
    public string[] Cards = new string[] {
        "../img/cards/bear_card.png", "../img/cards/cat_card.png", "../img/cards/flamingo_card.png", 
        "../img/cards/iguana_card.png", "../img/cards/jellyfish_card.png", "../img/cards/kangoroo_card.png", 
        "../img/cards/lion_card.png", "../img/cards/owl_card.png"
    };
    public int[] CardFaces = new int[7];
    Random random = new Random(DateTime.Now.Millisecond);
    public bool ButtonDisabled { get; set; } = false;
    public string backgroundimg { get; set; } = "";
    public int TotalUnwinnables { get; set; } = 0;


    protected override void OnInitialized()
    {
        // setup game
        TheDeck = new Deck(2, NumberOfRounds);
        CanStillPlay = true;
        GameWinnable = TheDeck.WinPossible(1);
        if (!GameWinnable)
            TotalUnwinnables++;

        // pick random card faces
        for (int i = 0; i < 7; i++)
            CardFaces[i] = random.Next(Cards.Length);

        // pick random background
        int imagenum = random.Next(0, 3);
        if (imagenum == 0)
            backgroundimg = "image1";
        else if (imagenum == 1)
            backgroundimg = "image2";
        else
            backgroundimg = "image3";

        base.OnInitialized();
    }

    public async void CardClicked(int card)     // called when a card is clicked
    {
        // change te card, check if they won/lost, and update the screen
        TheDeck.ChangeCard(card);
        UserWon = TheDeck.CheckWin();
        CanStillPlay = TheDeck.WinPossible(0);
        await JSRuntime.InvokeAsync<string>("CardFlipSound");
    }

    public void NextGame()                      // called when a game is complete
    {
        // reset game
        NumberOfRounds++;
        TheDeck = new Deck(2, NumberOfRounds);
        for (int i = 0; i < 7; i++)
            CardFaces[i] = random.Next(Cards.Length);
        Chevrons = new string[7];
        GameWinnable = TheDeck.WinPossible(1);
        if (!GameWinnable)
            TotalUnwinnables++;
        UserWon = false;
        CanStillPlay = true;
        ButtonDisabled = false;
        UnwinnableString = "";
    }

    public void OnDrop(Tuple<int, string> data)     // called when the user drops a chevron
    {
        // update array
        Chevrons[data.Item1] = data.Item2;
    }

    public void Unwinnable()                // called when a user declares unwinnable
    {
        // output correct string
        if (!GameWinnable)
        {
            UnwinnableString = "You are right! The game is unwinnable";
            UserWon = true;
            CorrectUnwinnables++;
        }
        else
        {
            UnwinnableString = "You are wrong! You can win this game!";
        }
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IJSRuntime JSRuntime { get; set; }
    }
}
#pragma warning restore 1591
