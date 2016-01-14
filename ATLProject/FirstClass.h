// FirstClass.h : CFirstClass ������

#pragma once
#include "resource.h"       // ������



#include "ATLProject_i.h"



#if defined(_WIN32_WCE) && !defined(_CE_DCOM) && !defined(_CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA)
#error "Windows CE ƽ̨(�粻�ṩ��ȫ DCOM ֧�ֵ� Windows Mobile ƽ̨)���޷���ȷ֧�ֵ��߳� COM ���󡣶��� _CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA ��ǿ�� ATL ֧�ִ������߳� COM ����ʵ�ֲ�����ʹ���䵥�߳� COM ����ʵ�֡�rgs �ļ��е��߳�ģ���ѱ�����Ϊ��Free����ԭ���Ǹ�ģ���Ƿ� DCOM Windows CE ƽ̨֧�ֵ�Ψһ�߳�ģ�͡�"
#endif

using namespace ATL;


// CFirstClass

class ATL_NO_VTABLE CFirstClass :
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CFirstClass, &CLSID_FirstClass>,
	public IDispatchImpl<IFirstClass, &IID_IFirstClass, &LIBID_ATLProjectLib, /*wMajor =*/ 1, /*wMinor =*/ 0>
{
public:
	CFirstClass()
	{
	}

DECLARE_REGISTRY_RESOURCEID(IDR_FIRSTCLASS)


BEGIN_COM_MAP(CFirstClass)
	COM_INTERFACE_ENTRY(IFirstClass)
	COM_INTERFACE_ENTRY(IDispatch)
END_COM_MAP()



	DECLARE_PROTECT_FINAL_CONSTRUCT()

	HRESULT FinalConstruct()
	{
		return S_OK;
	}

	void FinalRelease()
	{
	}

public:



	STDMETHOD(Add)(LONG para1, LONG para2, LONG* result);
};

OBJECT_ENTRY_AUTO(__uuidof(FirstClass), CFirstClass)
