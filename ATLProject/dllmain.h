// dllmain.h : 模块类的声明。

class CATLProjectModule : public ATL::CAtlDllModuleT< CATLProjectModule >
{
public :
	DECLARE_LIBID(LIBID_ATLProjectLib)
	DECLARE_REGISTRY_APPID_RESOURCEID(IDR_ATLPROJECT, "{E25B3F6B-AEAD-4F7C-B14F-95F953CA5F05}")
};

extern class CATLProjectModule _AtlModule;
