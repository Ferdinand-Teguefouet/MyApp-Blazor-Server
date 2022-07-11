using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApp.Pages
{
    public class MicrosoftLink : ComponentBase
    {
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(sequence: 0, elementName: "a");
            builder.AddAttribute(sequence: 1, name: "href", value: "http://www.microsoft.com");
            builder.AddContent(sequence: 2, textContent: "Allez chez Microsoft");
            builder.CloseElement();
            //base.BuildRenderTree(builder);
        }
    }
}
