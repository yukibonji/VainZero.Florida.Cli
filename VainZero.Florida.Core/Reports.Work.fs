namespace VainZero.Florida.Reports

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module Work =
  let create project content duration note =
    {
      案件 =
        project
      内容 =
        content
      工数 =
        duration
      備考 =
        note
    }
