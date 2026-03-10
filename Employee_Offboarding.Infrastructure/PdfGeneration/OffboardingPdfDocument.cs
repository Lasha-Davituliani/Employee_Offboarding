using QuestPDF.Infrastructure;
using Employee_Offboarding.Application.DTOs.Pdf;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using Employee_Offboarding.Domain.Entities;

namespace Employee_Offboarding.Infrastructure.PdfGeneration
{
    public class OffboardingPdfDocument : IDocument
    {
        private readonly PdfFormDataDto _model;
        private readonly string _georgianFontFamily;

        public OffboardingPdfDocument(PdfFormDataDto model)
        {
            _model = model;
            var fontPath = Path.Combine(Directory.GetCurrentDirectory(), "Fonts", "nino-mtavruli.ttf");
            if (File.Exists(fontPath))
            {
                FontManager.RegisterFont(File.OpenRead(fontPath));
                _georgianFontFamily = "BPG Nino Mtavruli";
            }
            else
            {
                _georgianFontFamily = "Arial";
            }
        }
        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
        public void Compose(IDocumentContainer container)
        {
            container
                .Page(page =>
                {
                    page.Margin(30);
                    page.DefaultTextStyle(x => x.FontFamily(_georgianFontFamily).FontSize(10));
                    page.Header().ShowOnce().Element(ComposeHeader);
                    page.Content().Element(ComposeContent);
                    page.Footer().AlignCenter().Text(x =>
                    {
                        x.Span(" ");
                        x.CurrentPageNumber();

                    });
                    });

        }

        void ComposeContent(IContainer container)
        {
            container.PaddingTop(20).Column(column =>
            {
                column.Spacing(25);
                column.Item().Element(ComposeInfoTable);
                foreach (var module in _model.Modules)
                {
                    column.Item().Element(c => ComposeModule(c, module));
                }
                column.Item().Element(ComposeEmployeeConfirmation);
            });
        }

        void ComposeEmployeeConfirmation(IContainer container)
        {
            if (!_model.EmployeeConfirmedAtUtc.HasValue) return;

            container.Border(1).BorderColor(Colors.Grey.Lighten2).Column(column =>
            {
                column.Item().Background(Colors.Grey.Lighten3).Padding(5).Text("თანამშრომლის დადასტურება").Bold();

                column.Item().Padding(10).Column(col =>
                {
                    col.Spacing(8);
                                        
                    col.Item().Text(text =>
                    {
                        text.Span("პასუხის თარიღი: ").SemiBold();
                        text.Span(_model.EmployeeConfirmedAtUtc?.ToString("yyyy-MM-dd HH:mm"));
                    });                    

                    if (!string.IsNullOrWhiteSpace(_model.ForceConfirmedByUsername))
                    {                        
                        col.Item().Text(text =>
                        {
                            text.Span("სტატუსი: ").SemiBold();
                            text.Span("დადასტურებულია HR-ის მიერ").FontColor(Colors.Orange.Medium).Bold();
                        });

                        col.Item().Text(text =>
                        {
                            text.Span("შემსრულებელი: ").SemiBold();
                            text.Span(_model.ForceConfirmedByUsername);
                        });
                    }
                    else
                    {                       
                        var statusText = _model.EmployeeAgreed == true ? "დადასტურებულია" : "უარყოფილია";
                        var statusColor = _model.EmployeeAgreed == true ? Colors.Green.Medium : Colors.Red.Medium;

                        col.Item().Text(text =>
                        {
                            text.Span("სტატუსი: ").SemiBold();
                            text.Span(statusText).FontColor(statusColor).Bold();
                        });
                    }                    

                    if (!string.IsNullOrWhiteSpace(_model.EmployeeComment))
                    {
                        col.Item().Text(text =>
                        {
                            text.Span("კომენტარი: ").SemiBold();
                            text.Span(_model.EmployeeComment).Italic();
                        });
                    }
                   
                });
            });
        }

        private void ComposeInfoTable(IContainer container)
        {
            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(130);
                    columns.RelativeColumn();
                    columns.ConstantColumn(130);
                    columns.RelativeColumn();
                });
                void InfoCell(string label, string value)
                {
                    table.Cell().Border(1).Background(Colors.Grey.Lighten3).Padding(5).Text(label).Bold();
                    table.Cell().Border(1).Padding(5).Text(value ?? "-");
                }

                InfoCell("თანამშრომელი:", _model.EmployeeFullName);
                InfoCell("პირადი ნომერი:", _model.PersonalNumber);
                InfoCell("პოზიცია:", _model.PositionTitle);
                InfoCell("შევსების თარიღი:", _model.CreatedAtUtc.ToString("yyyy-MM-dd"));
                InfoCell("სამუშაო ადგილი:", _model.WorkplaceLabel);
                table.Cell().Border(1).Background(Colors.Grey.Lighten3).Padding(5).Text("უშუალო ხელმძღვანელი:").Bold();
                table.Cell().ColumnSpan(1).Border(1).Padding(5).Text(_model.DirectManagerName);


            });
        }

        void ComposeModule(IContainer container, PdfFormDataDto.ModuleDto module)
        {
            container.Border(1).BorderColor(Colors.Grey.Lighten2).Column(column =>
            {
                column.Item().Background(Colors.Grey.Lighten3).Padding(5).Row(row =>
                {
                    row.RelativeItem().Text(module.DepartmentName).Bold();
                    if(module.DepartmentName == "უშუალო ხელმძღვანელი")
                    {
                        row.ConstantItem(150).AlignRight().Text($"შემსრულებელი: {_model.DirectManagerName}").FontSize(8).Italic();
                    }
                    else
                    {
                        row.ConstantItem(150).AlignRight().Text($"შემსრულებელი: {module.CompletedBy}").FontSize(8).Italic();
                    }

                });

                column.Item().Padding(5).Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn(4);
                        columns.RelativeColumn(2);
                        columns.RelativeColumn(3);
                    });

                    table.Header(header =>
                    {
                        header.Cell().Background(Colors.Grey.Lighten4).Padding(4).Text("საკითხი").Bold();
                        header.Cell().Background(Colors.Grey.Lighten4).Padding(4).Text("სტატუსი").Bold();
                        header.Cell().Background(Colors.Grey.Lighten4).Padding(4).Text("კომენტარები").Bold();
                    });

                    foreach (var item in module.Items)
                    {
                        table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(4).Text(item.Name);
                        table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(4).Text(TranslateItemStatus(item.Status));
                        table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(4).Text(item.TextValue);
                    }
                });

                if (!string.IsNullOrWhiteSpace(module.ManagerComment))
                {
                    column.Item().Padding(5).BorderTop(1).BorderColor(Colors.Grey.Lighten2)
                        .Text(text =>
                        {
                            text.Span("მენეჯერის კომენტარი: ").Bold();
                            text.Span(module.ManagerComment).Italic();
                        });
                }

            });
        }

        string TranslateItemStatus(ClearanceFormItemStatus status)
        {
            return status switch
            {
                ClearanceFormItemStatus.Pending => "დასადასტურებელია",
                ClearanceFormItemStatus.InReview => "განხილვაშია",
                ClearanceFormItemStatus.Confirmed => "დადასტურებულია",
                ClearanceFormItemStatus.Returned => "ჩაბარებულია",
                ClearanceFormItemStatus.NotReturned => "არ ჩაბარებულა",
                ClearanceFormItemStatus.NotHad => "არ უსარგებლია",
                ClearanceFormItemStatus.NoDebt => "არ ფიქსირდება დავალიანება",
                ClearanceFormItemStatus.HasDebt => "ფიქსირდება დავალიანება",
                ClearanceFormItemStatus.Cancelled => "გაუქმებულია",
                ClearanceFormItemStatus.NotCancelled => "არ არის გაუქმებული",
                ClearanceFormItemStatus.DidNotHave => "არ ჰქონდა (მინდობილობა)",
                ClearanceFormItemStatus.Yes => "კი",
                ClearanceFormItemStatus.No => "არა",
                ClearanceFormItemStatus.NotApplicable => "არ სარგებლობს",
                ClearanceFormItemStatus.InProgress => "მიმდინარე",
                ClearanceFormItemStatus.OverdueLoan => "ვადაგადაცილებული სესხი",
                ClearanceFormItemStatus.NotApplicableToCashier => "არ ვრცელდება სალაროს პასუხისმგებლობა",
                _ => status.ToString() // "უცნობის" ნაცვლად, დავაბრუნოთ enum-ის სახელი, შეცდომის ადვილად სანახავად
            };
        }

         void ComposeHeader(IContainer container)
        {
            container.PaddingBottom(15).Column(column =>
            {
                column.Item().Row(row =>
                {
                    row.AutoItem().Column(c =>
                    {
                        var logoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "company-logo.png");
                        if (File.Exists(logoPath))
                        {
                            c.Item().Height(15).Image(logoPath).FitHeight();
                        }
                        else
                        {
                            c.Item().Text("ემ ბი სი").FontSize(16).Bold().FontColor("#002d72");
                        }
                    });

                    row.RelativeItem().AlignRight().AlignBottom().PaddingBottom(5)
                        .Text($"#{_model.Id}")
                        .FontSize(12)
                        .SemiBold()
                        .FontColor(Colors.Grey.Medium);
                });
                column.Item().PaddingVertical(5).LineHorizontal(1).LineColor(Colors.Grey.Lighten2);

                column.Item().AlignCenter()
                    .Text("შემოვლის ბარათი")
                    .FontSize(24)
                    .Bold();
            });
        }
    }
}
