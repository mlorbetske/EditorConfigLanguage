using Microsoft.VisualStudio.Package;
using Microsoft.VisualStudio.TextManager.Interop;
using System.Runtime.InteropServices;

namespace EditorConfig
{
    [Guid("f99a05b5-311b-4772-92fc-6441a78ca26f")]
    public class EditorConfigLanguage : LanguageService
    {
        private LanguagePreferences preferences = null;

        public EditorConfigLanguage(object site)
        {
            SetSite(site);
        }

        public override Source CreateSource(IVsTextLines buffer)
        {
            return new EditorConfigSource(this, buffer, new EditorConfigColorizer(this, buffer, null));
        }

        public override TypeAndMemberDropdownBars CreateDropDownHelper(IVsTextView forView)
        {
            return null;
        }

        public override LanguagePreferences GetLanguagePreferences()
        {
            if (preferences == null)
            {
                preferences = new LanguagePreferences(Site, typeof(EditorConfigLanguage).GUID, Name);

                if (preferences != null)
                {
                    preferences.Init();

                    preferences.EnableCodeSense = true;
                    preferences.EnableMatchBraces = true;
                    preferences.EnableMatchBracesAtCaret = true;
                    preferences.EnableShowMatchingBrace = true;
                    preferences.EnableCommenting = true; ;
                    preferences.HighlightMatchingBraceFlags = _HighlightMatchingBraceFlags.HMB_USERECTANGLEBRACES;
                    preferences.LineNumbers = true;
                    preferences.MaxErrorMessages = 100;
                    preferences.AutoOutlining = true;
                    preferences.MaxRegionTime = 2000;
                    preferences.ShowNavigationBar = true;
                    preferences.InsertTabs = false;
                    preferences.IndentSize = 2;
                    preferences.ShowNavigationBar = true;

                    preferences.WordWrap = false;
                    preferences.WordWrapGlyphs = true;

                    preferences.AutoListMembers = true;
                    preferences.EnableQuickInfo = true;
                    preferences.ParameterInformation = true;
                }
            }

            return preferences;
        }

        public override IScanner GetScanner(IVsTextLines buffer)
        {
            return null;
        }

        public override AuthoringScope ParseSource(ParseRequest req)
        {
            return null;
        }

        public override string GetFormatFilterList()
        {
            return "EditorConfig File (*.editorconfig)|*.editorconfig";
        }

        public override string Name => ContentTypes.EditorConfig;

        public override void Dispose()
        {
            try
            {
                if (preferences != null)
                {
                    preferences.Dispose();
                    preferences = null;
                }
            }
            finally
            {
                base.Dispose();
            }
        }
    }
}
