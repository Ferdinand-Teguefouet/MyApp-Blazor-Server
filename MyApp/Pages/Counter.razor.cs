using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApp.Pages
{
    public class CounterBase : ComponentBase //,IDisposable
    {
        protected int currentCount = 0;

        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        //[Parameter]
        //public int value { get => currentCount; set => currentCount = value; }

        [CascadingParameter(Name = "Count")]public int InitialCount { get; set; }
        protected bool Loading { get; private set; }
        public string Color { get; private set; }

        protected async Task IncrementCount(MouseEventArgs e)
        {
            if (e.AltKey)
            {
                currentCount += 2;
            }
            else
            {
                currentCount++;
            }
            
            if (currentCount >= 20)
            {
                Color = "red";
            }
            else
            {
                Color = "white";
            }

            if (currentCount >= 18)
            {
                await JSRuntime.InvokeVoidAsync(identifier: "displayAlert", currentCount);
            }
        }


        [JSInvokable]
        public async Task IncrementByThree()
        {
            currentCount += 3;
            await InvokeAsync(StateHasChanged);
        }

        protected override void OnInitialized()
        {
            Console.WriteLine(value: "OnInitialized BEGIN");
            Loading = true;
            _ = Task.Delay(millisecondsDelay: 5000).ContinueWith(_ => { Loading = false; InvokeAsync(StateHasChanged); });
            base.OnInitialized();
            Console.WriteLine(value: "OnInitialized END");
        }


        protected override void OnParametersSet()
        {
            Console.WriteLine(value: "OnParametersSet BEGIN");
            currentCount = InitialCount;
            base.OnParametersSet();
            Console.WriteLine(value: "OnParametersSet END");
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var thisRef = DotNetObjectReference.Create(this); // this veut dire vers moi-même
                await JSRuntime.InvokeVoidAsync(identifier: "storeCounterComponent", thisRef);
            }
            Console.WriteLine(value: "OnAfterRender BEGIN");
            await base.OnAfterRenderAsync(firstRender);
            Console.WriteLine(value: "OnAfterRender END");
        }

        protected override bool ShouldRender()
        {
            Console.WriteLine(value: "ShouldRender BEGIN");
            Console.WriteLine(value: "ShouldRender END");
            return base.ShouldRender();
        }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            Console.WriteLine(value: "ShouldRender BEGIN");
            await base.SetParametersAsync(parameters);
            Console.WriteLine(value: "ShouldRender END");
        }


        public void Dispose()
        {
            Console.WriteLine(value: "DISPOSE");
        }
    }
}
