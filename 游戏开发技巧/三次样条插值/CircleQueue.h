#pragma once
#include "cocos2d.h"
USING_NS_CC;

template<class  T>
class CCircleQueue2
{
public:
	CCircleQueue2(int iLen)
	{
		m_iQuqueLen = iLen;
		m_lData.resize(m_iQuqueLen);
	};
public:
	int m_iQuqueLen;
	std::vector<T> m_lData;
public:
	void SetMaxLen(int iLen)
	{
		m_iQuqueLen = iLen;
		m_lData.resize(m_iQuqueLen);
	};

	void SetValue(int index, T value)
	{
		assert(index >= 0);
		index %= m_iQuqueLen;

		m_lData[index] = value;
	};

	const T &GetValue(int index)
	{
		assert(index >= 0);
		index %= m_iQuqueLen;

		return m_lData[index];
	};
};
