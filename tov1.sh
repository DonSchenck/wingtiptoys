oc patch route/wingtiptoys -p '{"spec":{"to":{"name":"wingtipserver"}}}'
oc patch route/wingtiptoys -p '{"spec":{"port":{"targetPort":"80"}}}'
