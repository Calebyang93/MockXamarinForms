<?xml version="1.0" encoding="UTF-8"?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:d="http://xamarin.com/schemas/2014/forms/design"
             xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
             mc:Ignorable="d"
             x:Class="XFApp1.Views.NewItemPage"
             Title="New Item">

    <ContentPage.Content>
        <StackLayout Spacing="3" Padding="15">
            <Label Text="Text" FontSize="Medium" />
            <Entry Text="{Binding Text, Mode=TwoWay}" FontSize="Medium" />
            <Label Text="Description" FontSize="Medium" />
            <Editor Text="{Binding Description, Mode=TwoWay}" AutoSize="TextChanges" FontSize="Medium" Margin="0" />
            <StackLayout Orientation="Horizontal">
                <Button Text="Cancel" Command="{Binding CancelCommand}" HorizontalOptions="FillAndExpand"></Button>
                <Button Text="Save" Command="{Binding SaveCommand}" HorizontalOptions="FillAndExpand"></Button>
            </StackLayout>
        </StackLayout>
    </ContentPage.Content>

</ContentPage>
