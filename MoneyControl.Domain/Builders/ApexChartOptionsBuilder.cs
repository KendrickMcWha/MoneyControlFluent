using ApexCharts;
using Microsoft.Extensions.Options;

namespace MoneyControl.Domain.Builders;
public static class ApexChartOptionsBuilder
{
    public static ApexChartOptions<TransactionMonth> BuildTransMonthOptionsStack()
    {
        return new ApexChartOptions<TransactionMonth>
        {
            Chart = new Chart
            {
                Stacked = true,
            },
            PlotOptions = new PlotOptions
            {
                Bar = new PlotOptionsBar
                {
                    Horizontal = false,
                    BorderRadius = 5,
                    BorderRadiusApplication = BorderRadiusApplication.Around,
                    BorderRadiusWhenStacked = BorderRadiusWhenStacked.All,
                }
            },
            Colors = new List<string> { "#5cb85c", "#d9534f" }
        };
    }

    public static ApexChartOptions<TransactionMonth> BuildTransMonthOptionsHorizontal()
    {
        return new ApexChartOptions<TransactionMonth>
        {
            Chart = new Chart
            {
                Stacked = true,
                StackType = StackType.Percent100,
                Toolbar = new Toolbar
                {
                    Show = false
                }
            },
            PlotOptions = new PlotOptions
            {
                Bar = new PlotOptionsBar
                {
                    Horizontal = true,
                }
            },
            Theme = new Theme { Mode = Mode.Light, Palette = PaletteType.Palette8 },

            Xaxis = new XAxis
            {
                Title = new AxisTitle
                {
                    Text = "Percentage",
                    Style = AxisTitleStyle14()
                },
                Labels = new XAxisLabels
                {
                    Style = AxisLabelStyle14()
                }
            },
            Yaxis = new List<YAxis> { new YAxis
                {
                     Title = new AxisTitle
                    {
                        Text = "Month",
                        Style = AxisTitleStyle14()
                    },
                    Labels = new YAxisLabels {
                        Style =  AxisLabelStyle14()
                        }
                }},
            Legend = new Legend
            {
                Position = LegendPosition.Right
            }
        };
    }
    public static ApexChartOptions<TransactionMonth> BuildTransMonthOptionsLine()
    {
        return new ApexChartOptions<TransactionMonth>
        {
            Chart = new Chart
            {
                Toolbar = new Toolbar
                {
                    Show = false
                },
                DropShadow = new DropShadow
                {
                    Enabled = true,
                    Color = "",
                    Top = 18,
                    Left = 7,
                    Blur = 10,
                    Opacity = 0.2d
                }
            },
            DataLabels = new ApexCharts.DataLabels
            {
                OffsetY = -6d
            },
            Grid = new Grid
            {
                BorderColor = "#e7e7e7",
                Row = new GridRow
                {
                    Colors = new List<string> { "#f3f3f3", "transparent" },
                    Opacity = 0.5d
                }
            },

            Colors = new List<string> { "#77B6EA", "#545454" },
   //ERROR         Markers = new Markers { Shape =  ShapeEnum.Circle, Size = 5, FillOpacity = new Opacity(0.8d) },
            Stroke = new Stroke { Curve = Curve.Smooth },
            Legend = new Legend
            {
                Position = LegendPosition.Top,
                HorizontalAlign = Align.Right,
                Floating = true,
                OffsetX = -5,
                OffsetY = -25
            },
        };    
    }

    public static ApexChartOptions<TransactionMonth> BuildTransMonthOptionsRange()
    {
        var options = new ApexChartOptions<TransactionMonth>();
        options.Yaxis = new List<YAxis>();
        options.Yaxis.Add(new YAxis
        {
            Labels = new YAxisLabels
            {
                Formatter = @"function(val) {
                              return '$' +val
                            }"
            }
        });
        return options;
    }
    private static AxisLabelStyle AxisLabelStyle14() => new AxisLabelStyle
                                                            {
                                                                FontSize = "14px"
                                                            };
    private static AxisTitleStyle AxisTitleStyle14() => new AxisTitleStyle
                                                            {
                                                                FontSize = "14px"
                                                            };

    


}
