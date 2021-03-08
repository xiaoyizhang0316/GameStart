using MHLab.Patch.Core.Client.Localization;

namespace MHLab.Patch.Launcher.Scripts.Localization
{
    public sealed class EnglishUpdaterLocalizedMessages : IUpdaterLocalizedMessages
    {
        //public string UpdateDownloadingArchive => "Downloading patch {0} to {1}...";
        //public string UpdateDownloadedArchive => "Downloaded patch archive {0}_{1}.";
        //public string UpdateDecompressingArchive => "Decompressing patch {0} to {1}...";
        //public string UpdateDecompressedArchive => "Decompressed patch {0} to {1}.";
        //public string UpdateUnchangedFile => "Unchanged file: {0}";
        //public string UpdateProcessingNewFile => "Adding new file: {0}";
        //public string UpdateProcessedNewFile => "Added new file: {0}";
        //public string UpdateProcessingDeletedFile => "Deleting file: {0}";
        //public string UpdateProcessedDeletedFile => "Deleted file: {0}";
        //public string UpdateProcessingUpdatedFile => "Updating file: {0}";
        //public string UpdateProcessedUpdatedFile => "Updated file: {0}";
        //public string UpdateProcessingChangedAttributesFile => "Fixing file attributes: {0}";
        //public string UpdateProcessedChangedAttributesFile => "Fixed file attributes: {0}";
        //public string NotAvailableNetwork => "Network is not available or connectivity is low/weak... Check your connection!";
        //public string NotAvailableServers => "Our servers are not responding... Wait some minutes and retry!";
        //public string UpdateProcessCompleted => "Updating process completed successfully!";
        //public string UpdateProcessFailed => "Updating process failed!";
        //public string UpdateRestartNeeded => "A restart is needed!";
        public string UpdateDownloadingArchive => "下载补丁 {0} 到 {1}...";
        public string UpdateDownloadedArchive => "已下载 {0}_{1}.";
        public string UpdateDecompressingArchive => "正在解压缩补丁 {0} to {1}...";
        public string UpdateDecompressedArchive => "已解压 {0} to {1}.";
        public string UpdateUnchangedFile => "未变更文件数目: {0}";
        public string UpdateProcessingNewFile => "添加新文件: {0}";
        public string UpdateProcessedNewFile => "已添加的新文件: {0}";
        public string UpdateProcessingDeletedFile => "删除文件: {0}";
        public string UpdateProcessedDeletedFile => "已删除文件: {0}";
        public string UpdateProcessingUpdatedFile => "更新文件: {0}";
        public string UpdateProcessedUpdatedFile => "已更新文件: {0}";
        public string UpdateProcessingChangedAttributesFile => "修正文件属性: {0}";
        public string UpdateProcessedChangedAttributesFile => "已修正文件属性: {0}";
        public string NotAvailableNetwork => "您的网络状况异常，请检查是否连接上因特网!";
        public string NotAvailableServers => "服务器暂时无响应，请稍后再试";
        public string UpdateProcessCompleted => "更新完成!";
        public string UpdateProcessFailed => "更新失败!";
        public string UpdateRestartNeeded => "需要重新启动!";
    }
}