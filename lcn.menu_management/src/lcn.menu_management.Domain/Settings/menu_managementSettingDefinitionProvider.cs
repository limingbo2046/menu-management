using Volo.Abp.Settings;

namespace lcn.menu_management.Settings
{
    public class menu_managementSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            /* Define module settings here.
             * Use names from menu_managementSettings class.
             */

            //添加到配置里面
            context.Add(
            new SettingDefinition(Settings.menu_managementSettings.SampleName, "默认样本1名称")//这里添加了一个配置和默认值，这样配置了就能用了
            );
        }
    }
}