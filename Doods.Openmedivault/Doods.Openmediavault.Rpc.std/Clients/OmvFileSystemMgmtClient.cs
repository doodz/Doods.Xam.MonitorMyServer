using System.Collections.Generic;
using System.Threading.Tasks;
using Doods.Openmediavault.Rpc.Std.Data.V4.FileSystem;
using Doods.Openmediavault.Rpc.Std.Interfaces;

namespace Doods.Openmediavault.Rpc.Std.Clients
{
    public class OmvFileSystemMgmtClient : OmvServiceClient
    {
        public OmvFileSystemMgmtClient(IRpcClient client) : base(client)
        {
            ServiceName = "FileSystemMgmt";
        }

        public Task<IEnumerable<OmvFilesystems>> GetFilesystems()
        {
            var request = NewRequest("enumerateFilesystems");

            var result = RunCmd<IEnumerable<OmvFilesystems>>(request);

            return result;
        }

        public Task<string> GetCandidatesBg()
        {
            var request = NewRequest("getCandidatesBg");

            var result = RunCmd<string>(request);

            return result;
        }

        public Task<string> CreateFileSystemBackground(BaseOmvFilesystems filesystems)
        {
            var request = NewRequest("create");
            request.Params = filesystems;

            var result = RunCmd<string>(request);

            return result;
        }
        public Task<OmvFilesystems> SetMountPoint()
        {
            var request = NewRequest("setMountPoint");

            var result = RunCmd<OmvFilesystems>(request);

            return result;
        }


        public Task<IEnumerable<OmvFilesystems>> GetMountCandidates()
        {
            var request = NewRequest("getMountCandidates");

            var result = RunCmd<IEnumerable<OmvFilesystems>>(request);

            return result;
        }

        public Task<string> GetListFileSystemBackground()
        {
            var request = NewRequest("getListBg");
            request.Params = new ParamsListFilter();

            var result = RunCmd<string>(request);

            return result;
        }

        public Task<bool> UmountFileSystem(BaseOmvFilesystems filesystem)
        {
            var request = NewRequest("umount");
            request.Params = new {id = filesystem.Devicefile, fstab = true};

            var result = RunCmd<bool>(request);

            return result;
        }

        public Task<bool> MountFileSystem(BaseOmvFilesystems filesystem)
        {
            var request = NewRequest("mount");
            request.Params = new {id = filesystem.Devicefile, fstab = true};

            var result = RunCmd<bool>(request);

            return result;
        }

        public Task<bool> DeleteFileSystem(OmvFilesystems filesystem)
        {
            var request = NewRequest("delete");
            request.Params = new {id = filesystem.Uuid};

            var result = RunCmd<bool>(request);

            return result;
        }
    }
}