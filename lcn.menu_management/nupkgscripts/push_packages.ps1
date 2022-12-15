. "./pack.ps1"

##推送包到包服务端

[xml]$commonPropsXml = Get-Content (Join-Path $rootFolder "common.props")#拿到common的版本号
$version = $commonPropsXml.Project.PropertyGroup.Version
Write-Host ("去到目录" +$packFolder+"推送包")
Set-Location $packFolder
# Publish all packages
foreach($project in $projects) {
    $projectName = $project.Substring($project.LastIndexOf("/") + 1)
    ## 第一次推包要手动上传 https://www.nuget.org/packages/manage/upload
    & dotnet nuget push ($projectName + "." + $version + ".nupkg") --api-key oy2ig3tkpbpyzj2iwf**remove me **6afntm6wwen24uhnyo3cnyayy4k4 --source https://api.nuget.org/v3/index.json
}


##回到解决方案
#Set-Location $rootFolder

#tf checkin common.props /noprompt /comment:"更新版本"
