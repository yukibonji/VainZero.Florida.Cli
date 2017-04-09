namespace FsYaml

module Yaml =
  let myDump<'x> obj =
    let yaml = Yaml.dump<'x> obj

    // Workaround the bug YamlDotNet can't handle linebreaks correctly.
    // https://github.com/aaubry/YamlDotNet/issues/246
    let yaml = yaml.Replace("\r\n\r\n", "\r\n")

    // Change folded style to literal style.
    let yaml = yaml.Replace(": >-", ": |").Replace(": >", ": |")

    yaml
