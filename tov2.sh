oc patch route/wingtiptoys -p '{"spec":{"to":{"name":"wingtiptoysv2"}}}'
oc patch route/wingtiptoys -p '{"spec":{"port":{"targetPort":"8080"}}}'
