//���ݲ�ͬ��ҳ����ز�ͬ��ģ��
const pageMap: Record<string, () => Promise<any>> = {
    StartPage_Index: () => import('./ts/StartPage/index'),
};

//����head�ϵ�data-page���������������ĸ�ģ��
const pageKey = document.head.dataset.page ?? 'unknown';
if (pageMap[pageKey]) {
    pageMap[pageKey]().then(mod => {
        mod.init();
    });
} else {
    console.warn(`[bundle] δ�ҵ� ${pageKey} ��Ӧ��ģ��`);
}