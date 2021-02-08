using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Corso10157.Customizations.TagHelpers
{
    public class RatingTagHelper : TagHelper
    {
        public double Value { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "span";
            // double value = (double)context.AllAttributes["value"].Value;
            // <i class="fas fa-star"></i>
            for (int i = 1; i <= 5; i++)
            {
                if (Value >= i)
                {
                    output.Content.AppendHtml("<i class=\"fas fa-star\"></i>");
                }
                else if (Value > i - 1)
                {
                    output.Content.AppendHtml("<i class=\"fas fa-star-half-alt\"></i>");
                }
                else
                {
                    output.Content.AppendHtml("<i class=\"far fa-star\"></i>");
                }
            }
        }
    }
}