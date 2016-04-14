# Uwp - Highlight on search

Finally you can create highlight on search in UWP

This project is only an example

The core of this feature is inside the method 

> SearchBox_OnTextChanged

but the main idea is inside the method

> CreateHighlightedParagraph

In a few words:

1. I use two *rich text block* inside a *ListView* and I fill them with some data
2. When you use the search box, you trigger the *SearchBox_OnTextChanged*
3. Now I replace all the found word with *Text Block* inside yellow *Border*
4. :thumbsup: Now the highlight functionality can be used on UWP

I know that can be implemented better but I search everywhere with no result :disappointed_relieved:
So I hope that can help you and I wish that Microsoft add something to simplify this feature :eyes:

If you like it put a shining :star:
