(*Classes*)
type Application = System.Windows.Forms.Application
type Window = System.Windows.Forms.Form
type Menu = System.Windows.Forms.MainMenu
type TextBox = ICSharpCode.TextEditor.TextEditorControl
type DockStyle = System.Windows.Forms.DockStyle
type OpenDialog = System.Windows.Forms.OpenFileDialog
type SaveDialog = System.Windows.Forms.SaveFileDialog
type MsgBox = System.Windows.Forms.MessageBox
type DialogResult = System.Windows.Forms.DialogResult
type Shortcut = System.Windows.Forms.Shortcut
type HighlightingManager = ICSharpCode.TextEditor.Document.HighlightingManager
type FileSyntaxModeProvider = ICSharpCode.TextEditor.Document.FileSyntaxModeProvider
type Directory = System.IO.Directory

[<System.STAThread>](*Without this attribute don't work system dialogs*)
[<EntryPoint>]
let main argv = 
    (*Load highlighting*)
    HighlightingManager.Manager.AddSyntaxModeFileProvider(new FileSyntaxModeProvider(Directory.GetCurrentDirectory()))
    
    (*Current file*)
    let file_name = ref ""
    
    (*Controls' definitions*)
    let window = new Window ()
    let menu = new Menu ()
    let text_box = new TextBox ()
    
    (*Menu tree's definition*)
    let file_menu = menu.MenuItems.Add("&File")
    let file_open_menu = file_menu.MenuItems.Add("&Open", fun _ _ -> 
        let dialog = new OpenDialog()
        if dialog.ShowDialog() = DialogResult.OK then
            file_name := dialog.FileName
            text_box.LoadFile(!file_name))
    file_open_menu.Shortcut <- Shortcut.CtrlO
    let file_save_menu = file_menu.MenuItems.Add("&Save", fun _ _ -> 
        if !file_name = "" then
            let dialog = new SaveDialog()
            if dialog.ShowDialog() = DialogResult.OK then
                file_name := dialog.FileName
        text_box.SaveFile(!file_name))
    file_save_menu.Shortcut <- Shortcut.CtrlS
    let file_save_as_menu = file_menu.MenuItems.Add("&Save as...", fun _ _ -> 
        let dialog = new SaveDialog()
        if dialog.ShowDialog() = DialogResult.OK then
            file_name := dialog.FileName
            text_box.SaveFile(!file_name))
    let assembly_menu = menu.MenuItems.Add("&Assembly")
    let check_assembly_menu = assembly_menu.MenuItems.Add("&Check", fun _ _ -> MsgBox.Show("Check function unreliased") |> ignore)
    let build_assembly_menu = assembly_menu.MenuItems.Add("&Build", fun _ _ -> MsgBox.Show("Build function unreliased") |> ignore)
    let start_assembly_menu = assembly_menu.MenuItems.Add("&Start", fun _ _ -> MsgBox.Show("Start function unreliased") |> ignore)

    (*Controls' properties*)
    text_box.Dock <- DockStyle.Fill
    window.Controls.Add(text_box)
    window.Menu <- menu
    
    (*Code run*)
    Application.Run(window)

    0
