/*
business start
用途：删除内容列表信息
*/
function deletecontentfromlist(redirecturl) {
    var alist = $('[id=a-delete]');
    /*为每个显示的“教室”绑定 删除事件*/
    alist.bind("click", function () {
        layer.confirm('是否删除"' + $(this).attr('delete-hint') + '"?', {
            btn: ['删除', '取消'], //按钮
            shade: [0.3, '#393D49'],//笼罩层
            closeBtn: false,
            a: $(this),
            title: '删除',
            time: 30000
        }, function () {
            var url = this.a.attr('url');
            var a = this.a;
            console.log('delete room url:' + url);

            $.post(url, function (data) {
                if (data.success == true) {
                    var deletetr = a.parent().parent();
                    deletetr.remove();
                    layer.msg('删除成功', { icon: 1, time: 1200 });
                    tryredict(redirecturl);
                } else
                    layer.msg('删除失败:' + data.message, { icon: 7 });
            })
        }, function () {
            //...取消按钮业务
        });
    });
}

/*删除教室时，如果当前页教室列表tbody没有元素，直接跳转到“教室管理”首页，以便刷新分页菜单显示正确分页项*/
function tryredict(redirecturl) {
  //  var tbody = $('table#grouplist tbody');
  //  if (tbody.children().length == 0)/*只剩下thead和空tbody则直接刷新页面，以便消除可能多余的分页链接*/
    // location.href = redirecturl;
    
    var totalcount = $('#grouplist').attr('total-count');
    var pageSize = $('#grouplist').attr('page-size');
    if (totalcount != "" || pageSize != "") {
        var result = totalcount % pageSize;
        if(result==1)//通过求余运算，判断清除多余分页链接,余数为1，需刷新列表
            location.href = redirecturl;
    }
    
    console.log(location.href);
}
/*end*/